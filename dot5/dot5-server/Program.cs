using System;
using NetMQ;

namespace dot5_server
{
    class Program
    {
        static string defaultIp = "127.0.0.1";
        static string defaultServerName = "server";

        static void Main(string[] args)
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

                if (!string.IsNullOrEmpty(newIP))
                {
                    newIP = string.Format("tcp://{0}", newIP);
                }
                else
                    newIP = defaultIp;
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

            Random rand = new Random();
            int port = 3000 + rand.Next(4000);
            string serverBinding = string.Format("tcp://{0}:{1}", newIP, port);

            using (var ctx = NetMQContext.Create())
            {
                using (var server = ctx.CreateDealerSocket())
                {
                    server.Options.SendTimeout = TimeSpan.FromMilliseconds(500);
                    server.Bind(serverBinding);
                    Console.WriteLine(string.Format("Server bound on {0}", serverBinding));

                    while (true)
                    {
                        var message = server.ReceiveString();
                        if (!string.IsNullOrEmpty(message))
                        {
                            server.Send(string.Format("{0} {1}", serverName, newIP));
                            Console.WriteLine("From Client: {0}", message);
                            break;
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
