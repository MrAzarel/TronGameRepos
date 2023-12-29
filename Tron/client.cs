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

        public static async void Connect(IPEndPoint IPEnd, string exchangeData)
        {
            UdpClient udpClient = new UdpClient();
            client clientUDP = new client();

            try
            {
                await clientUDP.ClientSend(udpClient, exchangeData, IPEnd);

                IPEndPoint remoteEP = null;

                await clientUDP.ClientReceive(udpClient, IPEnd, remoteEP);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }

        void CloseCLient(UdpClient udpClient)
        {
            udpClient.Close();
        }
        
        public async         
        Task
        ClientSend(UdpClient udpClient, string exchangeData, IPEndPoint IPEnd)
        {
                byte[] data = Encoding.UTF8.GetBytes(exchangeData);
                udpClient.Send(data, data.Length, IPEnd);
        }

        public async
        Task
        ClientReceive(UdpClient udpClient, IPEndPoint IPEnd, IPEndPoint remoteEP)
        {
            byte[] responseData = udpClient.Receive(ref remoteEP);

            string response = Encoding.UTF8.GetString(responseData);
            Console.WriteLine("Получен ответ от сервера: " + response);
        }

    }
}
