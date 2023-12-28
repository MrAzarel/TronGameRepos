using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace Tron
{
    internal class client
    {

        public static async void Connect(IPEndPoint IPEnd)
        {
            {
                UdpClient udpClient = new UdpClient(IPEnd);

                // Создаем IPEndPoint для хранения информации о клиентах
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                try
                {
                    Console.WriteLine("Сервер запущен. Ожидание подключений...");

                    while (true)
                    {
                        byte[] data = new byte[1024];
                        string msg = Console.ReadLine();
                        data = Encoding.UTF8.GetBytes(msg);
                        udpClient.Connect(IPEnd);
                        udpClient.Send(data, 1024);

                        // Преобразуем полученные данные в строку
                        string message = Encoding.UTF8.GetString(data);

                        // Выводим сообщение от клиента
                        Console.WriteLine("Получено сообщение от клиента: " + message);

                        // Отправляем ответ клиенту
                        string response = "Сообщение получено на сервере";
                        byte[] responseData = Encoding.UTF8.GetBytes(response);
                        udpClient.Send(responseData, responseData.Length, IPEnd);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
                finally
                {
                    // Закрываем UDP сокет
                    udpClient.Close();
                }
            }
        }

    }
}
