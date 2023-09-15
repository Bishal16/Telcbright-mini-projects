//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;

//namespace Observer_pattern
//{ 
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            WeatherStation weatherStation = new WeatherStation();

//            NewsAgency newsAgency1 = new NewsAgency("Alpha NA");
//            weatherStation.subscribe(newsAgency1.Update);

//            //NewsAgency newsAgency2 = new NewsAgency("Beta NA");
//            //weatherStation.Attach(newsAgency2);

//            weatherStation.Temperature = 31.1f;
//            weatherStation.Temperature = 32.2f;
//            weatherStation.Temperature = 33.3f;
//            weatherStation.Temperature = 34.4f;
//            ;
//        }
//    }

//    // weather station --->> observable/ subject
//    // News Agency     --->> observer
//    interface IObservable  // subject
//    {
//        void subscribe(Action<IObservable> updateMethod);  // could be used by the observer with the observable
//        void Notify();                    // notify observer about the changes
//    }



//    class WeatherStation : IObservable
//    {
//        private List<Action<IObservable>> _observers;
//        private float _temperature;

//        public float Temperature
//        {
//            get { return _temperature; }
//            set
//            {
//                _temperature = value;
//                Notify();
//            }
//        }

//        public WeatherStation()
//        {
//            _observers = new List<Action<IObservable>>();
//        }

//        public void subscribe(Action<IObservable> update)
//        {
//            _observers.Add(update);
//        }

//        public void Notify()
//        {
//            this._observers.ForEach(action =>
//            {
//                action(this);
//            });
//        }
//    }

//    class  NewsAgency// Observer
//    {
//        public String AgencyName { get; set; }

//        public NewsAgency(String name)
//        {
//            AgencyName = name;           
//        }

//        public void Update(IObservable subject)
//        {
//            WeatherStation weatherStation = subject as WeatherStation;
//            if (weatherStation != null)
//            {
//                Console.WriteLine("{0} reporting temperature {1} degree celcius",
//                    AgencyName,
//                    weatherStation.Temperature);
//                Console.WriteLine();
//            }
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Observer_pattern
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherStation weatherStation = new WeatherStation();

            NewsAgency newsAgency1 = new NewsAgency("Alpha NA");
            weatherStation.subscribe(newsAgency1);

            //NewsAgency newsAgency2 = new NewsAgency("Beta NA");
            //weatherStation.Attach(newsAgency2);

            weatherStation.Temperature = 31.1f;
            weatherStation.Temperature = 32.2f;
            weatherStation.Temperature = 33.3f;
            weatherStation.Temperature = 34.4f;
            ;
        }
    }

    // weather station --->> observable/ subject
    // News Agency     --->> observer
    interface IObservable  // subject
    {
        void subscribe(IObserver observer);  // could be used by the observer with the observable
        void Notify();                    // notify observer about the changes
    }

    interface IObserver
    {
        void Update(IObservable subject);
    }

    class WeatherStation : IObservable    // observable
    {
        private List<IObserver> _observers;
        private float _temperature;

        public float Temperature
        {
            get { return _temperature; }
            set { _temperature = value; Notify(); }
        }
        public WeatherStation()
        {
            _observers = new List<IObserver>();
        }

        public void subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o =>
            {
                o.Update(this);
            });
        }
    }

    class NewsAgency : IObserver  // Observer
    {
        public String AgencyName { get; set; }

        public NewsAgency(String name)
        {
            AgencyName = name;
        }

        public void Update(IObservable subject)
        {
            WeatherStation weatherStation = subject as WeatherStation;
            if (weatherStation != null)
            {
                Console.WriteLine("{0} reporting temperature {1} degree celcius",
                    AgencyName,
                    weatherStation.Temperature);
                Console.WriteLine();
            }
        }
    }
}
