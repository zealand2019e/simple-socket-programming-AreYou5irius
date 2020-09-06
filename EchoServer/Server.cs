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
            server = new TcpListener(port);

            // Start listening for client requests.
            server.Start();

            //accept tcp client

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Server Activated");

            //her benytter vi os af en client
            DoClient(client);

            //her stopper vi serveren
            server.Stop();
            Console.WriteLine("server stopped");

        }


        public static void DoClient(TcpClient client)
        {
            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);
            sw.AutoFlush = true; // enable automatic flushing


            //her siger vi hvis vi skriver "luk" så lukker forbindelsen og ellers ændre den bare beskeden til store bogstaver
            //while (true)
            //{
            //    string message = sr.ReadLine();

            //    Console.WriteLine("received message: " + message);

            //    if (message == "luk")
            //    {
            //        // Console.WriteLine("received message: " + message);
            //        break;
            //    }
            //    else if (message != null)
            //    {
            //        sw.WriteLine(message.ToUpper());
            //    }

            //}


            // her aflæses beskeden og bliver tildelt variable navnet message. herfra tager vi udgangspunkt i ordets længde.
            //  ******** SKRIV FORKLARING
            while (true)
            {
                string message = sr.ReadLine();
                Console.WriteLine("receved message: " + message);

                var wordCount = 0;
                for (var i = 0; i < message.Length; i++)
                    if (message[i] == ' ' || i == message.Length - 1)
                        wordCount++;
                Console.WriteLine("number of words in massage :" + wordCount);

            }


            //her lukker vi forbindelsen og client
            stream.Close();
            Console.WriteLine("net stream closed");
            client.Close();
            Console.WriteLine("connection to client closed");
        }


    }
}
