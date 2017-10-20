--Simple Web Server & Client--

Programming Language : C#
IDE: Microsoft Visual Studio 2017

--System Requirements--
1) Windows OS
2) .NET Framework v4.0.30319 or above version required (All latest versions will be in the given path only "C:\Windows\Microsoft.NET\Framework\v4.0.30319")


--Steps to Compile and execute Web Server --

1.Unzip the zip file in Desktop
2.Open command prompt (Run Server first)
3.Copy paste the following within quotes " path=%path%;C:\Windows\Microsoft.NET\Framework\v4.0.30319 "   (if path is not correct there will be an error when compiling saying 'csc.exe not recognized')
4.Go to directory of the Server folder " cd c:\Users\(username)\Desktop\1001505606_Akhil_Ghosh\Server "   (Give the username)
5.To compile " csc.exe Program.cs Request.cs Response.cs Server.cs "
6.Type "Program" in cmd and press enter to execute.
7.Open browser and enter " http://localhost:8080/index.html " to see the HTTP Request and HTTP Response message and RTT in cmd.
8.Enter " http://localhost:8080/(any name).html " to see the 404 error.


--Steps to Compile and execute Web Client--

1.Open another command Window
2.Copy paste the following within quotes " path=%path%;C:\Windows\Microsoft.NET\Framework\v4.0.30319 "
3.Go to directory of the Server folder " cd c:\Users\(username)\Desktop\1001505606_Akhil_Ghosh\Client "
5.To compile " csc.exe Program.cs "
6.Type "Program" in cmd and press enter to execute.
7.Give an input message in Client cmd. For example give input as " index.html" to get the html tags in client cmd 
otherwise will get a message saying "Page Not Found"


--Reference--
1.Compiling C# in the Command Prompt - https://www.youtube.com/watch?v=JQ58d3BEV_8
2.Server Client send/receive simple text -http://stackoverflow.com/questions/10182751/server-client-send-receive-simple-text
3.Sockets in C# - https://www.codeproject.com/articles/5252/sockets-in-c
4.Writing a WebSocket server in C# - https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server