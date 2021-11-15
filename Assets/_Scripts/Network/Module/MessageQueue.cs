using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class MessageQueue : SingletonMono<MessageQueue>
{
    private Queue<string> netQueue = new Queue<string>();

    protected override void Awake()
    {
        base.Awake();
    }


    public static void RecvModule(string msg)
    {
        instance.netQueue.Enqueue(msg);
    }

    public static bool CanGetNetworkMsg()
    {
        if (NetQueue.Count > 0)
            return true;
        return false;
    }

    public static string GetNetworkMsg()
    {
        if (NetQueue.Count > 0)
            return NetQueue.Dequeue();
        return "0";
    }


    public static void DebugNetPacket(string msg)
    {
        NetQueue.Enqueue(msg);
    }

    private static Queue<string> NetQueue
    {
        get
        {
            return instance.netQueue;
        }
    }

}

