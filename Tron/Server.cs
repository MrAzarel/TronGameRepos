    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Tron
{
    internal class Server
    {
        static Socket ServerUDP;
        public static EndPoint client1Endpoint;
        public static EndPoint client2Endpoint;
        public static int how_many_ready;
        public int how_many_connected = 0;

        public static void Host(IPEndPoint IPEnd)
        {
            Server serverMethods = new Server();
            ServerUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            ServerUDP.Bind(IPEnd);

            client1Endpoint = new IPEndPoint(IPAddress.Any, 0);
            client2Endpoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            Thread client1Thread = new Thread(()=> ListenForClient(client1Endpoint, serverMethods, "left"));
            Thread client2Thread = new Thread(() => ListenForClient(client2Endpoint, serverMethods, "right"));

            client1Thread.Start();
            client2Thread.Start();

        }

        void CloseServer(UdpClient udpServer)
        {
            udpServer.Close();
        }

        bool CheckMessage(string[] msg)
        {
            bool HasState, HasDirection, HasCoordinates, HasLife = false;
            float x, y = 0;

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
                if (float.TryParse(msg[1], out x) && float.TryParse(msg[2], out y))
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

        static void ListenForClient(object endpointObj , Server serverMethods, string client)
        {
            EndPoint remoteEP = (EndPoint)endpointObj;
            double x_before = 0;
            double y_before = 0;
            
            try
            {
                while (true)
                {

                    Console.WriteLine($"Client on {remoteEP}...");
                    byte[] buffer = new byte[256];
                    int receivedbytes = ServerUDP.ReceiveFrom(buffer, ref remoteEP);
                    Server UDPserver = new Server();

                    string message = Encoding.UTF8.GetString(buffer, 0, receivedbytes);

                    string[] msgData = message.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (msgData[0] == "connected")
                    {
                        Console.WriteLine("Received connected");
                        byte[] responseData = Encoding.UTF8.GetBytes(client);
                        ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, remoteEP);
                        serverMethods.how_many_connected = serverMethods.how_many_connected + 1;
                        Console.WriteLine($"Connected: {serverMethods.how_many_connected}");
                        if(client == "left")
                        {
                            client1Endpoint = remoteEP;
                        }
                        else if(client == "right")
                        {
                            client2Endpoint = remoteEP;
                        }
                    }
                    else
                    {
                        if (msgData[0] == "ready")
                        {
                            Console.WriteLine("Received ready");
                            
                            if (remoteEP.Equals(client1Endpoint))
                            {
                                Console.WriteLine($"{client1Endpoint} - client is ready");
                                byte[] responseData = Encoding.UTF8.GetBytes("ready");
                                ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, client2Endpoint);
                            }
                            else
                            {
                                 Console.WriteLine($"{client2Endpoint} - client is ready");
                                 byte[] responseData = Encoding.UTF8.GetBytes("ready");
                                 ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, client1Endpoint);
                            }
                        }
                        else if (msgData[0] == "dead")
                        {
                            if (remoteEP.Equals(client1Endpoint))
                            {
                                Console.WriteLine($"{client1Endpoint} - client is dead");
                                byte[] responseData = Encoding.UTF8.GetBytes("win");
                                ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, client2Endpoint);
                                serverMethods.Results("left won");
                            }
                            else
                            {
                                Console.WriteLine($"{client2Endpoint} - client is dead");
                                byte[] responseData = Encoding.UTF8.GetBytes("win");
                                ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, client1Endpoint);
                                serverMethods.Results("right won");
                            }

                        }
                        else
                        {
                            bool RightData = serverMethods.CheckMessage(msgData);
                            bool AdequateCoordinates = serverMethods.CheckCoordinates(x_before, y_before, Convert.ToDouble(msgData[1]), Convert.ToDouble(msgData[2]));

                            if (RightData && AdequateCoordinates)
                            {
                                Console.WriteLine("Получено правильное сообщение от клиента: " + message);
                                string response = message;
                                x_before = Convert.ToDouble(msgData[1]);
                                y_before = Convert.ToDouble(msgData[2]);

                                if (remoteEP.Equals(client1Endpoint))
                                {
                                    Console.WriteLine("tick");
                                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                                }
                                else
                                {
                                    Console.WriteLine("tick");
                                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                                    ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, client1Endpoint);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Неверные данные");
                                string response = $"Вы несете какую то чушь {remoteEP}";
                                byte[] responseData = Encoding.UTF8.GetBytes(response);
                                ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, remoteEP);
                            }
                            if (!AdequateCoordinates)
                            {
                                string response = "cheat";
                                byte[] responseData = Encoding.UTF8.GetBytes(response);
                                ServerUDP.SendTo(responseData, responseData.Length, SocketFlags.None, remoteEP);
                            }
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
               ServerUDP.Close();
            }
        }

        bool CheckCoordinates(double X_before, double Y_before, double x, double y)
        {
            if ((x - X_before <= 10) && (y - Y_before <= 10))
            {
                return true;
            }
            else
            {
            }
            return false;
        }

        void Results(string nameOfWinner)
        {
            string Path = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            Path = Path + "\records.txt";
            using (FileStream fstream = new FileStream(Path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] buffer = Encoding.Default.GetBytes(nameOfWinner);
                // запись массива байтов в файл
                fstream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("Текст записан в файл");
            }
        }
        
    }
    
}





