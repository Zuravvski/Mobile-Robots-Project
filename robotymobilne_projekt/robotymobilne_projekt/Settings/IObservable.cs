using System.Collections.Generic;

namespace robotymobilne_projekt.Settings
{
    public abstract class Observable
    {
        private List<IObserver> observers;

        public Observable()
        {
            observers = new List<IObserver>();
        }

        public void registerObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void unregisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void notifyObservers()
        {
            foreach(IObserver observer in observers)
            {
                observer.notify();
            }
        }
    }
}
