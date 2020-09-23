using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Apache.NMS.ActiveMQ.Commands;
using Grpc.Core;
using MedAppCore;
using MedAppCore.Models;
using MedAppCore.Services;
using Microsoft.Extensions.Logging;

namespace GrpcAppointment
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IBillingService _billingService;

        public GreeterService(ILogger<GreeterService> logger, IBillingService billingService)
        {
            _logger = logger;
            _billingService = billingService;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var res =  _billingService.CreateBill(new Bill
            {
                Id = Guid.NewGuid(),
            });

            if (res == 1)
            {
                return new HelloReply
                {
                    Message = true
                };
            }
            else
            {
                return new HelloReply
                {
                    Message = false
                };
            }            
        }
    }
}
