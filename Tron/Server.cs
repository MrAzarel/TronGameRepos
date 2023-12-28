    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class Server
    {
        public static async void Host(IPEndPoint IPEnd)
        {
            UdpClient udpServer = new UdpClient(IPEnd);

            // Создаем IPEndPoint для хранения информации о клиентах
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    // Получаем данные от клиента
                    byte[] data = udpServer.Receive(ref remoteEP);

                    // Преобразуем полученные данные в строку
                    string message = Encoding.UTF8.GetString(data);

                    // Выводим сообщение от клиента
                    Console.WriteLine("Получено сообщение от клиента: " + message);

                    // Отправляем ответ клиенту
                    string response = "Сообщение получено на сервере";
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    udpServer.Send(responseData, responseData.Length, remoteEP);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                // Закрываем UDP сокет
                udpServer.Close();
            }
        }
    }


}
