using System;
using System.Collections.Generic;
using System.Threading;

namespace RefactoringGuru.DesignPatterns.Observer.Conceptual
{
    public interface IObserver
    {
        // Receive update from subject
        void Update(EventInfo evt);
    }


    public class EventInfo
    {
        public String EventName { get; set; }

        public string EventData { get; set; }

    }

    public interface ISubject
    {
        // Attach an observer to the subject.
        void Attach(IObserver observer);

        // Detach an observer from the subject.
        void Detach(IObserver observer);

        // Notify all observers about an event.
        void Notify();
    }

    // The Subject owns some important state and notifies observers when the
    // state changes.
    public class Subject
    {
        // For the sake of simplicity, the Subject's state, essential to all
        // subscribers, is stored in this variable.
        public int State { get; set; } = -0;

        // List of subscribers. In real life, the list of subscribers can be
        // stored more comprehensively (categorized by event type, etc.).
        private List<IObserver> _observers = new List<IObserver>();

        // The subscription management methods.
        public void Attach(IObserver observer)
        {
            Console.WriteLine("Subject: Attached an observer.");
            this._observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            this._observers.Remove(observer);
            Console.WriteLine("Subject: Detached an observer.");
        }

        // Trigger an update in each subscriber.
        public void Notify(EventInfo evt)
        {
            Console.WriteLine("Subject: Notifying observers...");

            foreach (var observer in _observers)
            {
                //fired
                observer.Update(evt);
            }
        }

        // Usually, the subscription logic is only a fraction of what a Subject
        // can really do. Subjects commonly hold some important business logic,
        // that triggers a notification method whenever something important is
        // about to happen (or after it).
        public void SomeoneClickedSidebar()
        {
             this.State = new Random().Next(0, 10);

            Thread.Sleep(15);
            var evt = new EventInfo() { EventName = "windowOpen", EventData = "vessel:1122" };
            this.Notify(evt);
        }
    }

    // Concrete Observers react to the updates issued by the Subject they had
    // been attached to.
    class ConcreteObserverA : IObserver
    {
        public void Update(EventInfo evt)
        {

            Console.WriteLine("ConcreteObserverA: Reacted to the event . Event Name : "+evt.EventName.ToString() + " Event :"+evt.EventData.ToString());

        }
    }

    class ConcreteObserverB : IObserver
    {
        public void Update(EventInfo evt)
        {
            Console.WriteLine("ConcreteObserverB: Reacted to the event . Event Name : " + evt.EventName.ToString() + " Event :" + evt.EventData.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            var subject = new Subject();
            var observerA = new ConcreteObserverA();
            subject.Attach(observerA);

            var observerB = new ConcreteObserverB();
            subject.Attach(observerB);

            subject.SomeoneClickedSidebar();
            subject.SomeoneClickedSidebar();

            subject.Detach(observerB);

            subject.SomeoneClickedSidebar();
            Console.ReadLine();
        }
    }
}