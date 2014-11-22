using System;
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
            var baseAddress = args.Length >=1 
                ? args[0]
                : "127.0.0";
            var port = args.Length == 2
                ? args[1]
                : "5556";

            using (NetMQContext ctx = NetMQContext.Create())
            {
                using (var client = ctx.CreateDealerSocket())
                {
                   

                    var addresses = CreateAddresses(baseAddress, port);


                    foreach (var address in addresses)
                    {
                        //Console.WriteLine("Connecting to " + address);
                        client.Connect(address);
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

        private static IEnumerable<string> CreateAddresses(string baseAddress, string port)
        {
            return Enumerable.Range(0, 256)
                      .Select(ip => string.Format("tcp://{0}.{1}:{2}", baseAddress, ip, port))
                      .ToList();
        }

    }
}
