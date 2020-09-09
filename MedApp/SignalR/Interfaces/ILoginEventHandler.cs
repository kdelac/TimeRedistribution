using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Interfaces
{
    public interface ILoginEventHandler
    {
        void Publish(RecivedLoginMessageEvent eventMessage);
        void Subscribe(string subscriberName, Action<RecivedLoginMessageEvent> action);
        void Subscribe(string subscriberName, Func<RecivedLoginMessageEvent, bool> predicate, Action<RecivedLoginMessageEvent> action);
    }
}
