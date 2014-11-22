using System;
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
                    server.Bind("tcp://127.0.0.1:5556");

                    while (true)
                    {
                        var message = server.ReceiveString();
                        Console.WriteLine("From Client: {0}", message);
                        server.Send("Hello,");
                    }
                }
            }
        }
    }
}
