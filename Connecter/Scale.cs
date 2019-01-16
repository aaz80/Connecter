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
                const string handshake = 
@"<NET_LOGIN>
<USR=Cigiemme>
<PWD=Cigiemme>
<BUF=2000>";
                const string connoption = 
@"<OPTION_CONNECTION>
<TOTAL_CONTROL = FALSE>
<ENABLE_TX_TOT_PLU = TRUE>
<ENABLE_TX_ALL_CODEPAGE_NUMBERS = 1>
<ENABLE_WRT_ARC_NOT_SM = TRUE>
<ENABLE_TX_FINESTAMPA_PROD=TRUE>
<ENABLE_FINESTAMPA_TOT_1 = TRUE>
<END_OPTION_CONNECTION>";
                const string sendmessagetoscale = 
@"<SHOW_MESSAGE = MSG_OK>
<S0 = dialog title>
<S1 = text 1 line of the message>
<S2 = text 2 line of the message>
<END_SHOW_MESSAGE>";


                try
                {
                    ScaleSocket.Connect(ipEndpoint);
                    Console.WriteLine("Socket created to {0}", ScaleSocket.RemoteEndPoint.ToString());
                    byte[] sendmsg = Encoding.ASCII.GetBytes(handshake);
                    int n = ScaleSocket.Send(sendmsg);
                    Console.WriteLine(n);
                    sendmsg = Encoding.ASCII.GetBytes(connoption);
                    n = ScaleSocket.Send(sendmsg);
                    Console.WriteLine(n);
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

        public int SendData(string data)
        {
            if (this.CreateSocket()) {
                byte[] datar = new byte[2000];
                Console.WriteLine("Send data");
                byte[] sendmsg = Encoding.ASCII.GetBytes(data);
                int n = ScaleSocket.Send(sendmsg);
                int m = ScaleSocket.Receive(datar);
                this.CloseSocket();
                return m;
                }
            else Console.WriteLine("Doesn't create connection");
            return 1;
        }
    }
}