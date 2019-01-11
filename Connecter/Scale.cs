using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Connecter
{
    public enum ScaleType { RBS, Bizerba, Bilanciai };
    public abstract class Scale
    {
        public String Name { get; }
        public String IP { get; }
        public ScaleType Type { get; }

        private Socket ScaleSocket;
        private int Port = 1025;

        public Scale(string name, string ip, ScaleType type)
        {
            Name = name;
            IP = ip;
            Type = type;
            Console.WriteLine("Call Scale constructor");
        }

        public bool CreateSocket()
        {
            {
                IPAddress ipAdress = System.Net.IPAddress.Parse(IP);
                IPEndPoint ipEndpoint = new IPEndPoint(ipAdress, Port);
                ScaleSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    ScaleSocket.Connect(ipEndpoint);
                    Console.WriteLine("Socket created to {0}", ScaleSocket.RemoteEndPoint.ToString());
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Socket didn't create reason {0}", e.ToString());
                    return false;
                }

                Console.ReadKey();

            }
        }

        public void CloseSocket()
        {
            ScaleSocket.Shutdown(SocketShutdown.Both);
            ScaleSocket.Close();
            Console.WriteLine("Close socket");
        }

        public void SendData()
        {
            if (this.CreateSocket()) {
                Console.WriteLine("Send data");
                byte[] sendmsg = Encoding.ASCII.GetBytes("This is from Client\n");
                int n = client.Send(sendmsg);
                int m = client.Receive(data);
                this.CloseSocket();
                }
            else Console.WriteLine("Doesn't create connection");
        }
    }
}