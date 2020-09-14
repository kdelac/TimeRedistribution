using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SignalR.Interfaces;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace SignalR.Handlers
{
    public class ReciveLoginEventHandler : ILoginEventHandler, IDisposable
    {
        private readonly Subject<RecivedLoginMessageEvent> _subject;
        private readonly Dictionary<string, IDisposable> _subscribers;

        

        public ReciveLoginEventHandler(
            )
        {
            _subject = new Subject<RecivedLoginMessageEvent>();
            _subscribers = new Dictionary<string, IDisposable>();
        }

        public void Publish(RecivedLoginMessageEvent eventMessage)
        {
            _subject.OnNext(eventMessage);
        }

        public void Subscribe(string subscriberName, Action<RecivedLoginMessageEvent> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _subject.Subscribe(action));
            }
        }

        public void Subscribe(string subscriberName, Func<RecivedLoginMessageEvent, bool> predicate, Action<RecivedLoginMessageEvent> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _subject.Where(predicate).Subscribe(action));
            }
        }

        public void Dispose()
        {
            if (_subject != null)
            {
                _subject.Dispose();
            }

            foreach (var subscriber in _subscribers)
            {
                subscriber.Value.Dispose();
            }
        }        
    }
}