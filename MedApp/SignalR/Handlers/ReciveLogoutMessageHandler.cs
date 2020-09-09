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
    public class ReciveLogoutMessageHandler : ILogoutEventHandler, IDisposable
    {
        private readonly Subject<ReciveLogoutMessageEvent> _subject;
        private readonly Dictionary<string, IDisposable> _subscribers;

        public ReciveLogoutMessageHandler()
        {
            _subject = new Subject<ReciveLogoutMessageEvent>();
            _subscribers = new Dictionary<string, IDisposable>();
        }

        public void Publish(ReciveLogoutMessageEvent eventMessage)
        {
            _subject.OnNext(eventMessage);
        }

        public void Subscribe(string subscriberName, Action<ReciveLogoutMessageEvent> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _subject.Subscribe(action));
            }
        }

        public void Subscribe(string subscriberName, Func<ReciveLogoutMessageEvent, bool> predicate, Action<ReciveLogoutMessageEvent> action)
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
