using RealEstateSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateSample
{
    public class Subject : ISubject
    {

        private readonly static ISubject _instance = new Subject();

        private List<IObserver> observers = new List<IObserver>();

        public static ISubject GetInstance()
        {
            return _instance;
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify(House house)
        {
            observers.ForEach(x => x.Update(house));
        }
    }

    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(Observer observer);
        void Notify(House house);
    }

    public class Observer : IObserver
    {
        public string ObserverName { get; private set; }
        public Observer(string name)
        {
            this.ObserverName = name;
        }
        public void Update(House house)
        {
           
        }
    }

    public interface IObserver
    {
        void Update(House house);
    }
}
