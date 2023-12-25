using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;

public class User : MonoBehaviour
{
    static Socket client;

    public void Connect(string ip)
    {
        IPEndPoint IPend = new IPEndPoint(IPAddress.Parse("10.102.237.179"), 11000);
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(IPend);
        string name = System.Environment.MachineName;
        Console.WriteLine($"Подключено к: {client.RemoteEndPoint}");
        //string msg = "";
    }
}
