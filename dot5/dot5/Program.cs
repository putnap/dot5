using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NetMQ;
using NetMQ.Sockets;

namespace dot5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string baseAddress = args.Length >= 1
                ? args[0]
                : "127.0.0";

            using (NetMQContext ctx = NetMQContext.Create())
            {
                using (DealerSocket client = ctx.CreateDealerSocket())
                {
                    IEnumerable<IPAddress> addresses = CreateAddresses(baseAddress);

                    foreach (IPAddress address in addresses)
                    {
                        var a = string.Format("tcp://{0}:{1}", address, 5556);
                        client.Connect(a);
                    }

                    while (true)
                    {
                        string secret = client.ReceiveString();
                        Console.WriteLine("Secret " + secret);
                    }

                    Console.ReadLine();
                }
            }
        }


        private static IEnumerable<IPAddress> CreateAddresses(string baseAddress)
        {
            return Enumerable.Range(0, 256)
                .Select(ip => IPAddress.Parse(baseAddress + "." + ip))
                .ToList();
        }
    }
}