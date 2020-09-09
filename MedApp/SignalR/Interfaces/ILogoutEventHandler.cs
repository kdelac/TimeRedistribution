using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Interfaces
{
    public interface ILogoutEventHandler
    {
        void Publish(ReciveLogoutMessageEvent eventMessage);
        void Subscribe(string subscriberName, Action<ReciveLogoutMessageEvent> action);
        void Subscribe(string subscriberName, Func<ReciveLogoutMessageEvent, bool> predicate, Action<ReciveLogoutMessageEvent> action);
    }
}
