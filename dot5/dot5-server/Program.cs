using System;
using System.Diagnostics;
using NetMQ;

namespace dot5_server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = NetMQContext.Create())
            {
                using (var server = ctx.CreateDealerSocket())
                {
                    server.Bind("tcp://127.0.0.1:5556");
                    server.Send("Hello,");
                    //var message = server.ReceiveString();

                    //if (count%1000 == 0)
                    //{
                    //    //Console.WriteLine("From Client ({0}): {1}", count, message);
                    //    Console.WriteLine("Msgs/s: {0}", count / sw.Elapsed.TotalSeconds);
                    //}

                }
            }
        }
    }
}
