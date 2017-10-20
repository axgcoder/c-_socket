//WebServer 
//Name: Akhil Ghosh
//UTA ID: 1001505606

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTP_Server
{
    public class Request
    {
        //parameters
        public String Type { get; set; }
        public String URL { get; set; }
        public String Host { get; set; }

        public String Referer { get; set; }
        private Request(String type,String url , String host,String referer)
        {
            Type = type;
            URL = url;
            Host = host;
            Referer = referer;

        }
        public static Request GetRequest(String request)
        {
            
            if (String.IsNullOrEmpty(request))
                return null;

            String[] tokens = request.Split(' ','\n');
            String type = tokens[0];
            String url = tokens[1];
            String host = tokens[4];
            String referer = "";
            for(int i=0;i< tokens.Length; i++)
            {
                if(tokens[i] == "Referer:")
                {
                    referer = tokens[i + 1];
                    break;
                }
            }
            //Console.WriteLine(String.Format("{0} {1} @ {2} \nReferer: {3}", type, url, host, referer));
            return new Request(type,url,host, referer);
        }

        public static Request GetPage(String request)
        {
            String type;
            String url, host, referer;
            if (request.Contains("index.html"))
            {
                 type = "GET";
                 url = "/";
                 host = "text/html";
                 referer = "";
            }
            else
            {
                 type = "GET";
                 url = "/invalid";
                 host = "text/html";
                 referer = "";

            }

            return new Request(type, url, host, referer);
        }
    }
}
