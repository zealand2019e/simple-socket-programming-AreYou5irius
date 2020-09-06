using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
{
    class Client
    {
        public static void Start()
        {
            TcpClient socket = null;

            Int32 port = 7777;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            socket = new TcpClient(localAddr.ToString(), port);

            // Get a stream object for reading and writing
            NetworkStream stream = socket.GetStream();

            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);


            sw.WriteLine("Hej");
            sw.Flush();

            string line = sr.ReadLine();

            Console.WriteLine(line);

        }
    }
}
