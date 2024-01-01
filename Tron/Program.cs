using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint IPend = new IPEndPoint(IPAddress.Parse("10.102.160.170"), 556);
            Server.Host(IPend);

            //while (true)
            //{
            //string message = "connected";
            //client.Connect(IPend, message);
            //}
        }
    }
}
