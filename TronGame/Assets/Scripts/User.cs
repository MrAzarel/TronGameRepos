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
    static byte[] data = new byte[256];
    string getedData;

    Thread dataGetThread;
    Thread dataSendThread;

    public void Connect(string ip)
    {
        IPend = new IPEndPoint(IPAddress.Parse(ip), 556);
        client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        client.Connect(IPend);

        byte[] message = Encoding.UTF8.GetBytes("connected");
        client.SendTo(message, message.Length, SocketFlags.None, IPend);

        MainMenu.isConnected = true;

        Thread thread = new Thread(startSide);

        thread.Start();
    }

    void startSide()
    {
        dataGetThread = new Thread(dataProcessing);
        dataSendThread = new Thread(sendData);
        string start = "";
        while (true)
        {
            if (start != "" && (start.Split(' ')[0] == "left" || start.Split(' ')[0] == "right"))
            {
                WaitingMenu.startSide = start;
                dataGetThread.Start();
                dataSendThread.Start();
                break; 
            }
            else
            {
                int res = client.ReceiveFrom(data, ref IPend);
                start = Encoding.UTF8.GetString(data, 0, res);
            }
        }
    }

    string getData()
    {
        string message;
        int res = client.ReceiveFrom(data, ref IPend);
        message = Encoding.UTF8.GetString(data, 0, res);
        return message;
    }

    void sendData()
    {
        while (true)
        {
            if (WaitingMenu.isGameStarted)
            {
                byte[] message = Encoding.UTF8.GetBytes(WaitingMenu.dataToSend);
                client.SendTo(message, message.Length, SocketFlags.None, IPend);
            }
        }
    }

    public static void sendMessage(string messageToSend)
    {
        byte[] message = Encoding.UTF8.GetBytes(messageToSend);
        client.SendTo(message, message.Length, SocketFlags.None, IPend);
    }

    void dataProcessing()
    {
        while (true)
        {
            getedData = getData();
            if (getedData.Split(' ')[0] == "ready")
            {
                WaitingMenu.isEnemyReady = true;
                WaitingMenu.readyCount++;
            }
            else if (getedData.Split(' ')[0] == "start")
            {
                WaitingMenu.isGameStarted = true;
            }
            else if (getedData.Split(' ')[0] == "w" || getedData.Split(' ')[0] == "s" || getedData.Split(' ')[0] == "d" || getedData.Split(' ')[0] == "a")
            {
                Enemy.allData = getedData;
            }
            else if (getedData.Split(' ')[0] == "win")
            {
                Debug.Log("win");
                Restart.isWin = true;
            }
        }
    }
}
