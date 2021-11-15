using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Send
{
    public static void Result(string json)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("1#!");
        sb.Append(json);
        sb.Append("#!<EOF>");
        SocketManager.Send(sb.ToString());
    }
}

//Send.Result(json)필요한 시점에 하시면됩니다.