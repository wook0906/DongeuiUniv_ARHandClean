using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class SocketManager 
{
    public int port = 10152;
    public bool isDomain = false;

    public string ip= "3.34.173.85";

    public static SocketManager instance;
    private Socket clientSocket;

    private List<StateObject> stateObjectList = new List<StateObject>();

    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);

    private static String response = String.Empty;

    private byte[] syncMsg = new byte[1024];

    private string myIp;


    private static Socket ClientSocket
    {
        get
        {
            return instance.clientSocket;
        }

        set
        {
            instance.clientSocket = value;
        }
    }

    public static string MyIp
    {
        get
        {
            return instance.myIp;
        }

        set
        {
            instance.myIp = value;
        }
    }
    public void Start()
    {
        // ManualResetEvent instances signal completion.
        instance = this;
        try
        {
            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());
            myIp = IPHost.AddressList[0].ToString();
            IPAddress ipAddress;

            if (isDomain)
                ipAddress = Dns.GetHostAddresses(ip)[0];
            else
                ipAddress = IPAddress.Parse(ip);
            Debug.Log(ipAddress.ToString());

            IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Connect to the remote endpoint.
            clientSocket.BeginConnect(remoteEP,
                new AsyncCallback(ConnectCallback), GetRestStateObject());
            bool isConnected = connectDone.WaitOne(5000, true);
            if (!isConnected)
            {
                clientSocket.Close();
                throw new ApplicationException("Failed to connect server.");
            }
        }

        catch (Exception e)
        {
            Debug.Log("서버연결에 실패 " + e.Message);
        }

    }

    private void ConnectCallback(IAsyncResult ar)
    {
        try
        {

            clientSocket.EndConnect(ar);
            Receive();
            //Console.WriteLine("Socket connected to {0}", clientSocket.RemoteEndPoint.ToString());

            // Signal that the connection has been made.
            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }


    public static void Send(String data)
    {
        //Debug.Log(  "   Send: "+data);
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.UTF8.GetBytes(data);

        // Begin sending the data to the remote device.
        instance.clientSocket.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), null);

    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            sendDone.Set();
        }
        catch (Exception e)
        {
            //Console.WriteLine(e.ToString());
        }

        //stateobject 사용해제
        StateObject so = (StateObject)ar.AsyncState;
        so.isUsed = false;
    }

    public static string GetMyIP()
    {
        return MyIp;
    }

    public void Receive()
    {
        try
        {

            // Create the state object.
            StateObject state = GetRestStateObject();

            // Begin receiving the data from the remote device.
            clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReceiveCallback), state);
        }
        catch (Exception e)
        {
            //Console.WriteLine(e.ToString());
        }
    }

    public string ReceiveSync()
    {
        try
        {
            int byteCount = clientSocket.Receive(syncMsg);
            return Encoding.UTF8.GetString(syncMsg, 0, byteCount);
        }
        catch (Exception e)
        {
            //Console.WriteLine("ReceiveSync Error:{0}", e.Message);
        }
        return "error";
    }

    public static bool IsConnected()
    {
        if (ClientSocket == null)
            return false;
        return ClientSocket.Connected;
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        String content = String.Empty;
        StateObject state = (StateObject)ar.AsyncState;
        try
        {

            int bytesRead = clientSocket.EndReceive(ar);

            if (bytesRead > 0)
            {
                // 더 많은 데이터가있을 수 있음.
                // 현재까지의 데이터를 저장한다.
                state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));

                // EOF를 체크해서 없으면 더 받는다. 있으면 반응한다.
                // 몇바이트 보내는데 쪼개질일이 있겠냐.. 
                content = state.sb.ToString();
                string[] contents = Regex.Split(content, "<EOF>");

                int checkEOF = content.IndexOf("<EOF>");//EOF가 있어야 정상패킷이다.


                if (checkEOF > -1)
                {
                    for (int i = 0; i < contents.Length; i++)
                    {
                        //받아서 메세지 큐에 처박아둔다.
                        MessageQueue.RecvModule(contents[i]);
                    }
                    state.sb.Remove(0, state.sb.Length);

                }
                else
                {
                    clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }

            }
            else
            {
                if (state.sb.Length > 1)
                {
                    response = state.sb.ToString();
                    Debug.Log(response + "ReseiveCallBack의 state.sb.Length > 1일때 뜨는 로그");
                    //여기에 모듈 연결해주면 끝..
                }
                receiveDone.Set();
            }

        }
        catch (Exception e)
        {
            //Console.WriteLine(e.ToString());
        }
        state.isUsed = false;
        Receive();
    }

    private StateObject GetRestStateObject()
    {
        for (int i = 0; i < stateObjectList.Count; i++)
        {
            if (stateObjectList[i].isUsed == false)
            {
                stateObjectList[i].isUsed = true;
                return stateObjectList[i];

            }
        }
        StateObject stateObject = new StateObject();
        stateObjectList.Add(stateObject);
        stateObject.isUsed = true;
        return stateObject;
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Send 101");
        Send("101");
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();
    }
}

public class StateObject
{
    // Size of receive buffer.
    public const int BufferSize = 4096;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();
    public bool isUsed = false;
}