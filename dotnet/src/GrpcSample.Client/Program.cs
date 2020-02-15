using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcSample.Web;

namespace GrpcSample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string Name = ".NET Client";
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true); //don't want to worry with HTTPS stuff
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);

            var r = await client.SayHelloAsync(new HelloRequest {Name = Name});
            Console.WriteLine(r.Message);
            
            var replyStream = client.LotsOfReplies(new HelloRequest {Name = Name});

            await foreach (var reply in replyStream.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(reply.Message);
            }
        }
    }
}