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
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    await socket.ConnectAsync(IPEnd);
                    Console.WriteLine($"Подключение к {IPEnd} установлено");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw ex;
                }
            }
        }

    }
}
