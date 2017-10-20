//WebServer  
//Name: Akhil Ghosh
//UTA ID: 1001505606
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT_NO = 8080;
            const string SERVER_IP = "127.0.0.1";
         
            Console.WriteLine("Starting: Creating Socket object");
            while (true)
            {
                try
                {
                    //---create a TCPClient object at the IP and port no.---
                    TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
                    NetworkStream networkStream = client.GetStream();
                    Console.WriteLine("\n--Client Details--");
                    Console.WriteLine("Socket Family: {0}", client.Client.AddressFamily);
                    Console.WriteLine("Socket Type: {0}", client.Client.SocketType);
                    Console.WriteLine("Proctol: {0}", client.Client.ProtocolType);
                    Console.WriteLine("Successfully connectedto {0}", SERVER_IP);
                    //---data to send to the server---
                    Console.WriteLine("Enter the name of web page (Example: index.html ) :");
                    string sendingMessage ="";

                    while (string.IsNullOrEmpty(sendingMessage))
                    {
                         sendingMessage = Console.ReadLine();
                    }
                    
                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(sendingMessage);

                        //---send the text---
                        Console.WriteLine("Sending : " + sendingMessage);
                    networkStream.Write(bytesToSend, 0, bytesToSend.Length);

                        //---read back the text---
                        byte[] bytesToRead = new byte[client.ReceiveBufferSize];
                        int bytesRead = networkStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
                        Console.WriteLine("\nData Received from Server: \n" + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
                        Console.WriteLine("Press Enter to continue");
                        Console.ReadLine();
                        client.Close();
                    

                }
               catch
                {
                    Console.WriteLine("\n--Remote Server not found /disconnected--");
                  
                }
            }
        }
    }
}
