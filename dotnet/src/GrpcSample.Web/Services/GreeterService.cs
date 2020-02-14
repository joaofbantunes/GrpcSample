using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcSample.Web
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Got message from {name}, will respond.", request.Name);
            return Task.FromResult(new HelloReply {Message = $"Hi {request.Name}!"});
        }

        public override async Task LotsOfReplies(HelloRequest request,
            IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Got message from {name}, will start streaming responses.", request.Name);

            var intervalBetweenMessages = TimeSpan.FromSeconds(2);

            await responseStream.WriteAsync(new HelloReply {Message = $"Hey there {request.Name}!"});
            await Task.Delay(intervalBetweenMessages, context.CancellationToken);

            await responseStream.WriteAsync(new HelloReply {Message = "Long time no see!"});
            await Task.Delay(intervalBetweenMessages, context.CancellationToken);

            await responseStream.WriteAsync(new HelloReply {Message = "How's it going?"});
            await Task.Delay(intervalBetweenMessages, context.CancellationToken);

            await responseStream.WriteAsync(new HelloReply {Message = "In this sample, the server streams multiple responses to the client."});
            await Task.Delay(intervalBetweenMessages, context.CancellationToken);
            
            await responseStream.WriteAsync(new HelloReply {Message = "But it could also be the other way around or even both ways!"});
            await Task.Delay(intervalBetweenMessages, context.CancellationToken);
            
            await responseStream.WriteAsync(new HelloReply {Message = "Cyaz!"});
        }
    }
}