//WebServer  
//Name: Akhil Ghosh
//UTA ID: 1001505606

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public class Server
    {
        public const String MSG_DIR = "/root/msg/";
        public const String WEB_DIR = "/root/web/";
        public const String VERSION = "HTTP/1.1";
        public const String NAME = " AG HTTP Server v1";
        //to check port if it is running or not
        private bool port_running = false;
        //TCP Listener 
        private TcpListener listener;

        //create constructor
        public Server(int port)
        {
            //Create Server Socket
            listener = new TcpListener(IPAddress.Any, port);
            Console.WriteLine("Creating Socket object");
            Console.WriteLine("\n--Server Details--");
            Console.WriteLine("Server Name: {0}", NAME);
            Console.WriteLine("Socket Family: {0}",listener.Server.AddressFamily);
            Console.WriteLine("Socket Type: {0}",listener.Server.SocketType);
            Console.WriteLine("Proctol: {0}",listener.Server.ProtocolType);
        }
        //to start server  using thread
        public void Start()
        {
            //create a new ThreadStart delegate. 
            //The delegate points to a method that will be executed by the new thread.
            //Pass this delegate as a parameter when creating a new Thread instance. 
            //Finally, call the Thread.Start method to run  method (in this case Run) on background.

            Thread serverThread = new Thread(new ThreadStart(Run));
            serverThread.Start();
        }

        private void Run()
        {
            //opens the server port
            port_running = true;
            listener.Start();
            while (port_running)
            {
                Console.WriteLine("\nWaiting for connection...");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client connected");
                
                HandleClient(client);
                client.Close();
            }
            port_running = false;
            listener.Stop();
        }

        private void HandleClient(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream());
            NetworkStream networkStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            // get client details
            IPEndPoint endPoint = (IPEndPoint)client.Client.RemoteEndPoint;
            IPAddress ipAddress = endPoint.Address;
            IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
            Console.WriteLine("\n--Client Details--");
            Console.WriteLine("Host Name: {0} ", hostEntry.HostName);
            Console.WriteLine("Socket Family: {0} ", ipAddress.AddressFamily);
            try
            { 
                int bytesRead = networkStream.Read(buffer, 0, client.ReceiveBufferSize);
            string msg = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            if (msg.Contains("GET /"))
            {
                Console.WriteLine("\n--HTTP Request:-- \n\n" + msg);
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();
                // Begin timing.
                stopwatch.Start();
                // Stop timing.
                stopwatch.Stop();

                //make request
                Request req = Request.GetRequest(msg);

                Response resp = Response.From(req);
                // Write result.
                Debug.WriteLine(req);
                resp.Post(client.GetStream());
                Console.WriteLine("Round Trip Time- {0}", stopwatch.Elapsed);

            }
            else
            {
                // Create new stopwatch.
                Stopwatch stopwatch = new Stopwatch();
                // Begin timing.
                stopwatch.Start();


                Console.WriteLine("\nMessage From Client: " + msg);
                if (msg.Contains("index.html"))
                {
                    Request req = Request.GetPage(msg);
                    Response resp = Response.From(req);
                    // Stop timing.
                    stopwatch.Stop();
                    resp.PostToClient(client.GetStream());
                    // Stop timing.
                    stopwatch.Stop();
                    Console.WriteLine("Sending response to Client: ");
                    Console.WriteLine("Round Trip Time- {0}", stopwatch.Elapsed);
                }
                else
                {
                    msg = "404.html";
                    Request req = Request.GetPage(msg);
                    Response resp = Response.From(req);
                    resp.PostToClient(client.GetStream());

                    byte[] sendToClient = Encoding.ASCII.GetBytes("Page Not Found");
                    byte[] bytesFrom = new byte[client.ReceiveBufferSize];
                    networkStream.Write(sendToClient, 0, sendToClient.Length);
                    // Stop timing.
                    stopwatch.Stop();
                        Console.WriteLine("Sending response to Client: ");
                        Console.WriteLine("Round Trip Time- {0}", stopwatch.Elapsed);
                }

                //---send data---
                // networkStream.Write(sendToClient, 0, sendToClient.Length);

            }
        }
            catch
            {
                Console.WriteLine("\n--Client Disconnected--");
            }

        }
    }
}
