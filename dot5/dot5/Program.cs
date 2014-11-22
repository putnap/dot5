﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;

namespace dot5
{
    class Program
    {
        static void Main(string[] args)
        {
            var ipAddressAndPort = "127.0.0.1:5557";
            if (args.Any())
            {
                ipAddressAndPort = args[0];
            }

            using (NetMQContext ctx = NetMQContext.Create())
            {
                using (var client = ctx.CreateRouterSocket())
                {
                    client.Options.SendTimeout = TimeSpan.FromSeconds(10);
                    Console.WriteLine("Connecting to " + ipAddressAndPort);
                    client.Connect("tcp://" + ipAddressAndPort);
                    string m2 = client.ReceiveString();
                    Console.WriteLine("From Server: {0}", m2);
                    Console.ReadLine();
                }
            }

        }
    }
}
