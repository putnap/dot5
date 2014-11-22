using System;
using NetMQ;
using NetMQ.Sockets;

namespace dot5_server
{
    internal class Program
    {
        private static string defaultIp = "127.0.0.1";
        private static string defaultServerName = "server";

        private static void Main(string[] args)
        {
            string newIP = string.Empty;
            string serverName = string.Empty;
            if (args.Length > 0)
            {
                newIP = args[0];
            }
            else
            {
                Console.Write("Please enter a server ip: ");
                newIP = Console.ReadLine();

                if (string.IsNullOrEmpty(newIP))
                {
                    newIP = defaultIp;
                }
            }

            if (args.Length > 1)
            {
                serverName = args[1];
            }
            else
                while (string.IsNullOrEmpty(serverName))
                {
                    Console.Write("Please enter a server name: ");
                    serverName = Console.ReadLine();
                }

            if (string.IsNullOrEmpty(serverName))
                serverName = defaultServerName;

            int port = 5556;
            string serverBinding = string.Format("tcp://{0}:{1}", newIP, port);

            using (NetMQContext ctx = NetMQContext.Create())
            {
                using (DealerSocket server = ctx.CreateDealerSocket())
                {
                    server.Bind(serverBinding);
                    Console.WriteLine("Server bound on {0}", serverBinding);

                    while (true)
                    {
                        server.Send(string.Format("{0} {1}", serverName, newIP));

                        Console.WriteLine("SENT MESSAGE");
                        
                        break;
                    }
                }
            }
            Console.ReadLine();
        }
    }
}