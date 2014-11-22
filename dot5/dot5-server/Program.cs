using System;
using NetMQ;

namespace dot5_server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You must pass a secret as the first command line argument.");
                Environment.Exit(1);
            }

            var secret = args[0];
            var address = "127.0.0.1:5556";

            if (args.Length > 1)
            {
                address = args[1];
            }

            using (var ctx = NetMQContext.Create())
            {
                using (var server = ctx.CreateDealerSocket())
                {
                    server.Bind("tcp://" + address);
                    server.Send(secret);
                }
            }
        }
    }
}
