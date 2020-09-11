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


            //her sætter vi TcpListener til port 7777 ig IP adressen til den lokale ip
            Int32 port = 7777;
            IPAddress localAddr = IPAddress.Loopback;

            TcpListener server = new TcpListener(localAddr, port);

            // Her Starter vi serveren (altså skaber forbindelse til IP og Port)  (listening for client requests.)
            server.Start();

            //Her er den sat op så den kan tage imod flere clienter samtidig 
            //først angiver vi at serveren skal tage imod vores client
            // så siger vi hvis der er clienter på serveren skal den køre DoClient, hvis der ikke er nogle clienter på stopper serveren.
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
            //her holder vi styr på hvilke client nr clienten er.
            int Nr = clientNr;

            // Her bliver der oprettet en stream på clienten så der skal skrives til den og fra den. (Get a stream object for reading and writing)
            NetworkStream stream = client.GetStream();

            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);
            sw.AutoFlush = true; // enable automatic flushing

            //her fortæller vi at serveren skal aflæse den besked vi skriver.
            //når beskeden bliver læst tæller den ord u´dfra det kriterie at et ord slutter med et mellemrum og at beskeden skal læses til ende
            //(den fulde længde og at det sidste ord ikke har et mellemrum til sidst)
            //og skrives der "luk" afbrydes forbindelsen (til stream og client)
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
