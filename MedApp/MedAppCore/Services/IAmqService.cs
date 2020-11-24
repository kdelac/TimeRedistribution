using Apache.NMS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MedAppCore.Services
{
    public interface IAmqService
    {
        IObjectMessage ReciveEvent(string queueName);
        bool SendEvent<TEntity>(TEntity model, string queueName);
        bool SendEventTopic(string message, string queueName);
    }
}
