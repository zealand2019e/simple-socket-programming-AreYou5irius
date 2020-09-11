using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServer
{
    class Server
    {
        private static int clientNr = 1;

        public static void Start()
        {


            //Set the TcpListener on port 7777. 
            Int32 port = 7777;
            IPAddress localAddr = IPAddress.Loopback;

            // TcpListener server = new TcpListener(port);
            TcpListener server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            do
            {
                TcpClient client = server.AcceptTcpClient();

                Console.WriteLine("Server Activated - client nr : " + clientNr);
                Task.Run(() =>
                {
                    //her forøger vi client nr for hver gang en ny client bliver oprettet
                    DoClient(client, clientNr++);

                });
                if (clientNr < 1)
                {
                    break;

                }

            } while (clientNr > 0);

            //her stopper vi serveren
            server.Stop();
            Console.WriteLine("server stopped");

        }



        public static void DoClient(TcpClient client, int clientNr)
        {
            int Nr = clientNr;
            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();

            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);
            sw.AutoFlush = true; // enable automatic flushing


            while (true)
            {
                string message = sr.ReadLine();
                Console.WriteLine($"receved message from Client: {Nr} Message: {message}");

                var wordCount = 0;
                for (var i = 0; i < message.Length; i++)
                    if (message[i] == ' ' || i == message.Length - 1)
                        wordCount++;
                if (message == "luk")
                {
                    Console.WriteLine($"client nr: {Nr} is leaving");
                    break;

                }
                //Console.WriteLine("number of words in message : " + wordCount);

            }

            //her lukker vi forbindelsen og client
            stream.Close();
            Console.WriteLine("net stream closed");
            client.Close();
            Console.WriteLine("connection to client closed");

        }




    }
}
