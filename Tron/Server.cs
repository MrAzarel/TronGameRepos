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
            using (Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                try
                {
                    server_socket.Bind(IPEnd);
                    Console.WriteLine(server_socket.LocalEndPoint);
                    await server_socket.ReceiveAsync();
                    Console.WriteLine("Client connected");
                }
                catch (Exception ex) { 
                    Console.WriteLine(ex.ToString()); 
                }

            }
        }
    }
}
