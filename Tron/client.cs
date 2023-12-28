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
            // Создаем UDP клиент
            UdpClient udpClient = new UdpClient();

            try
            {
                while (true)
                {
                    Console.Write("Введите сообщение для сервера: ");
                    string message = Console.ReadLine();

                    byte[] data = Encoding.UTF8.GetBytes(message);

                    udpClient.Send(data, data.Length, IPEnd);

                    IPEndPoint remoteEP = null;
                    byte[] responseData = udpClient.Receive(ref remoteEP);

                    string response = Encoding.UTF8.GetString(responseData);
                    Console.WriteLine("Получен ответ от сервера: " + response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
            finally
            {
                // Закрываем UDP клиент
                udpClient.Close();
            }
        }
    }
}
