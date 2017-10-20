//WebServer 
//Name: Akhil Ghosh
//UTA ID: 1001505606

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on port 8080");
            Server server = new Server(8080); // initialise the port number
            server.Start();
        }
    }
}
