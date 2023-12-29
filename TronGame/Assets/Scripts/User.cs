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
using System.Threading;
using Unity.VisualScripting;

public class User : MonoBehaviour
{
    static Socket client;
    static EndPoint IPend;
    static byte[] data = new byte[1024];
    string getedData;
    public static string dataToSend;
    public static bool isGameStarted = false;
    public static bool isConnected = false;

    private void Update()
    {
        if (isGameStarted)
        {
            sendData();
        }
    }

    public void Connect(string ip)
    {
        IPend = new IPEndPoint(IPAddress.Parse(ip), 556);
        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.Connect(IPend);

        byte[] message = Encoding.UTF8.GetBytes("Client");
        client.SendTo(message, IPend);

        isConnected = true;

        Thread thread = new Thread(startGame);
        thread.Start();
    }

    void startGame()
    {
        string start = "";
        while (true)
        {
            if (start == "start")
            {
                byte[] message = Encoding.UTF8.GetBytes("GameStarted");
                client.SendTo(message, IPend);
                isGameStarted = true;
                break;
            }
            else
            {
                int res = client.Receive(data, 0, 1000, 0);
                start = Encoding.UTF8.GetString(data);
            }
        }
    }

    public static string getData()
    {
        string message;
        int res = client.Receive(data, 0, 1000, 0);
        message = Encoding.UTF8.GetString(data);
        return message;
    }

    void sendData()
    {
        byte[] message = Encoding.UTF8.GetBytes(dataToSend);
        client.SendTo(message, IPend);
    }

    public static void sendMessage(string messageToSend)
    {
        byte[] message = Encoding.UTF8.GetBytes(messageToSend);
        client.SendTo(message, IPend);
    }
}
