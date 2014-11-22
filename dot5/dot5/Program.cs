using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetMQ;
using System.Net;
using NetMQ.Sockets;

namespace dot5
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = args.Length >= 1
                ? args[0]
                : "127.0.0";
            var port = args.Length == 2
                ? args[1]
                : "5556";

            using (NetMQContext ctx = NetMQContext.Create())
            {
                using (var client = ctx.CreateDealerSocket())
                {
                    var addresses = CreateAddresses(baseAddress);


                    //var m = client.ReceiveMessage();

                    foreach (var address in addresses)
                    {
                        //Console.WriteLine("Connecting to " + address);
                        ConnectToIp(client, address);
                    }
                    while (true)
                    {
                        //client.Poll();
                        //var messages = client.ReceiveStringMessages();
                        var secret = client.ReceiveString();
                        Console.WriteLine("Secret " + secret);
                        //client.SendMore("");
                        //client.SendMore("Hello");

                    }

                    Console.ReadLine();
                }

            }

        }

        private static void ConnectToIp(DealerSocket socket, IPAddress address)
        {
            var addresses = new[] { 5556 }.Concat(Enumerable.Range(3000, 4001))
                .Distinct()
                .Select(port => string.Format("tcp://{0}:{1}", address.ToString(), port))
                .ToList();

            addresses.ForEach(socket.Connect);
        }

        private static IEnumerable<IPAddress> CreateAddresses(string baseAddress)
        {
            return Enumerable.Range(0, 256)
                .Select(ip => IPAddress.Parse(baseAddress + "." + ip))
                .ToList();
        }

    }
}
