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
                using (var server = ctx.CreateResponseSocket())
                {
                    server.Bind("tcp://192.168.1.10:5556");

                    int count = 0;

                    var sw = Stopwatch.StartNew();
                    
                    while (true)
                    {
                        var message = server.ReceiveString();

                        if (count%1000 == 0)
                        {
                            //Console.WriteLine("From Client ({0}): {1}", count, message);
                            Console.WriteLine("Msgs/s: {0}", count / sw.Elapsed.TotalSeconds);
                        }

                        server.Send("Hello,");

                        count++;
                    }
                }
            }
        }
    }
}
