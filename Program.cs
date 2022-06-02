using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp3
{
    //SERVER
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];



            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipA = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipA, 11000);

            Socket listener = new Socket(ipA.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            string data;

            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();


                data = null;
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }

                }

                Console.WriteLine("Text received : {0}", data);

                byte[] msg = Encoding.ASCII.GetBytes("Your message is received");

                handler.Send(msg);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        
        
        }
    }
}
