using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static ServerData serverData = new ServerData();
        static string msg = string.Empty;
        static void Main(string[] args)
        {
            Console.WriteLine("Start server...");
            try
            {
                serverData.socket.Bind(serverData.iPEndPoint);
                serverData.socket.Listen(10);
                
                Task.Factory.StartNew(() => Connect());

                SetMsg();
                SendMsg();


               // serverData.socketClient.Shutdown(SocketShutdown.Both);
               // serverData.socketClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Connect()
        {
            while (true)
            {
                serverData.socketClient = serverData.socket.Accept();
                serverData.socketClientsList.Add(serverData.socketClient);

                serverData.socketClient.Send(Encoding.Unicode.GetBytes("Welcome on server!"));
            }
        }
        static void SetMsg()
        {
            Console.WriteLine("Enter message:");
            msg = Console.ReadLine();
        }
        static void SendMsg()
        {
            foreach (var item in serverData.socketClientsList)
            {
                item.Send(Encoding.Unicode.GetBytes(msg));
            }
        }
    }
}
