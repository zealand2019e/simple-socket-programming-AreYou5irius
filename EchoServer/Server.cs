using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer
{
    class Server
    {
       public static void Start()
        {


            TcpListener server = null;


            //Set the TcpListener on port 7777. 
                Int32 port = 7777;
                IPAddress localAddr = IPAddress.Loopback;

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                //accept tcp client

                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Server Activated");


                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                StreamReader sr = new StreamReader(stream);
                StreamWriter sw = new StreamWriter(stream);
                sw.AutoFlush = true; // enable automatic flushing

                string message = sr.ReadLine();

                while (true)
                {

                Console.WriteLine("received message: " + message);
                    if (message != null)
                    {
                        sw.WriteLine(message.ToUpper());

                    }
                }

                stream.Close();
                Console.WriteLine("net stream closed");
                client.Close();
                Console.WriteLine("connection to client closed");
                server.Stop();
                Console.WriteLine("server stopped");

                
            }

    }
}
