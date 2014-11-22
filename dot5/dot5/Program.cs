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
            using (NetMQContext ctx = NetMQContext.Create())
            {
                Console.ReadLine();
                using (var client = ctx.CreateDealerSocket())
                {
                    client.Connect("tcp://127.0.0.1:5556");

                    while (true)
                    {
                        client.Send("Hello");

                        string m2 = client.ReceiveString();
                        Console.WriteLine("From Server: {0}", m2);
                        Console.ReadLine();
                    }
                }
            }

        }
    }
}
