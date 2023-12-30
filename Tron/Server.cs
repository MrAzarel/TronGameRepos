    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class Server
    {
        public static void Host(IPEndPoint IPEnd)
        {
            UdpClient udpServer = new UdpClient(IPEnd);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            Server serverUDP = new Server();
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            try
            {
                while (true)
                {
                    byte[] data = udpServer.Receive(ref remoteEP);
                    string message = Encoding.UTF8.GetString(data);

                    string[] msgData = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (msgData[0] == "ready")
                    {

                    }
                    else
                    {
                        bool RightData = serverUDP.CheckMessage(msgData);

                        if (msgData[0] == "close")
                        {
                            Console.WriteLine("Закрываем подключение");
                            serverUDP.CloseServer(udpServer);
                            return;
                        }

                        if (RightData)
                        {
                            Console.WriteLine("Получено правильное сообщение от клиента: " + message);
                            string response = $"Правильное сообщение получено на сервере: {message} от клиента: {remoteEP}";
                            byte[] responseData = Encoding.UTF8.GetBytes(response);
                            udpServer.Send(responseData, responseData.Length, remoteEP);
                        }
                        else
                        {
                            Console.WriteLine("Неверные данные");
                            string response = $"Вы несете какую то чушь {remoteEP}";
                            byte[] responseData = Encoding.UTF8.GetBytes(response);
                            udpServer.Send(responseData, responseData.Length, remoteEP);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                udpServer.Close();
            }
        }

        void CloseServer(UdpClient udpServer)
        {
            udpServer.Close();
        }

        bool CheckMessage(string[] msg)
        {
            bool HasState, HasDirection, HasCoordinates, HasLife = false;
            int x, y = 0;

            if (msg.Length >= 4)
            {
                if (msg[0] == "w" || msg[0] == "a" || msg[0] == "s" || msg[0] == "d")
                {
                    HasDirection = true;
                }
                else
                {
                    HasDirection = false;
                }
                if (int.TryParse(msg[1], out x) && int.TryParse(msg[2], out y))
                {
                    HasCoordinates = true;
                }
                else
                {
                    HasCoordinates = false;
                }
                if (msg[3] == "true" || msg[3] == "false")
                {
                    HasLife = true;
                }
                if (HasDirection && HasCoordinates && HasLife)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

    


}
