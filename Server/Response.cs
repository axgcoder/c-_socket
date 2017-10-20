//WebServer  
//Name: Akhil Ghosh
//UTA ID: 1001505606

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HTTP_Server
{
    public class Response
    {
        private String status;
        private Byte[] data = null;
        private String mime;
        //respone need
        private Response(String status,String mime, Byte[] data)
        {
            this.status = status;
            this.mime = mime;
            this.data = data;

        }

        public static Response From(Request request)
        {
            if (request == null)
                return MakeNullRequest();

            if (request.Type == "GET")
            {
                String file = Environment.CurrentDirectory + Server.WEB_DIR + request.URL;
                FileInfo f = new FileInfo(file);
                if (f.Exists && f.Extension.Contains("."))
                {
                    return MakeFromFile(f); 
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(f + "/");
                    if (!di.Exists)
                        return MakePageNotFound();

                    FileInfo[] files = di.GetFiles();

                    foreach(FileInfo ff in files)
                    {
                        String n = ff.Name;
                        if (n.Contains("default.html")|| n.Contains("index.html"))
                        //f = ff;
                        return MakeFromFile(ff);
                     }
                }
             }
            else
                return MakeMethodNotALlowed();
            return MakePageNotFound();
            
        }


        private static Response MakeFromFile(FileInfo f)
        {
      
            FileStream fs = f.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("200 OK", "html/text",d);
        }

        private static Response MakeNullRequest()
        {
            String file = Environment.CurrentDirectory + Server.MSG_DIR + "400.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("404 Bad Request","html/text",d);
        }

        private static Response MakePageNotFound()
        {
            String file = Environment.CurrentDirectory + Server.MSG_DIR + "404.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("404 Page Not Found", "html/text", d);
        }

        private static Response MakeMethodNotALlowed()
        {
            String file = Environment.CurrentDirectory + Server.MSG_DIR + "405.html";
            FileInfo fi = new FileInfo(file);
            FileStream fs = fi.OpenRead();
            BinaryReader reader = new BinaryReader(fs);
            Byte[] d = new Byte[fs.Length];
            reader.Read(d, 0, d.Length);
            fs.Close();
            return new Response("405 Method Not Allowed", "html/text",d);
        }

        public void Post(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n",Server.VERSION, status, Server.NAME,mime,data.Length) );
            Console.WriteLine("--HTTP Response--:\n");
            Console.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n", Server.VERSION, status, Server.NAME, mime, data.Length));
            //---send data---
            stream.Write(data,0,data.Length);
        }

        public void PostToClient(NetworkStream stream)
        {
            StreamWriter writer = new StreamWriter(stream);
            Byte[] resp = Encoding.ASCII.GetBytes(String.Format("\n--Response--\n\n{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n", Server.VERSION, status, Server.NAME, mime, data.Length));
            writer.WriteLine(String.Format("{0} {1}\r\nServer: {2}\r\nContent-Type: {3}\r\nAccept-Ranges: bytes\r\nContent-Length: {4}\r\n", Server.VERSION, status, Server.NAME, mime, data.Length));
           int length = data.Length + resp.Length;
            byte[] combined = new byte[length];
            data.CopyTo(combined, 0);
            resp.CopyTo(combined, data.Length);
            //---send data---
            stream.Write(combined, 0, combined.Length);
            
        }
    }
}
