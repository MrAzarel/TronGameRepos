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
            IPEndPoint IPend = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 555);
            Server.Host(IPend);
            Console.Read();
        }
    }
}
