
using System;
using System.Collections.Generic;

namespace Assignment1
{
    class RailwayCrossingLogger {
        private List<CarDetails> CarsData;
        private static readonly Object LockObj = new Object();
        private static RailwayCrossingLogger instance;

        public static RailwayCrossingLogger Instance
        {
            get {
                if (instance == null)
                {
                    lock (LockObj)
                    {
                        if(instance == null)
                        {
                            instance = new RailwayCrossingLogger();
                        }
                    }
                }
                return instance; 
            }
        }

        private RailwayCrossingLogger()
        {
            this.CarsData = new List<CarDetails>();
        }

        public void AddDetailsToLogger(Car car) {
            this.CarsData.Add(new CarDetails(car.Name, car.Signal.Name,DateTime.Now));
        }
        public void PrintLoggerDetails()
        {
            Console.WriteLine("================Details of Cars Stopped at Railway Crossings===============");
            foreach (var item in this.CarsData)
            {
                item.PrintDetails();
            }
        }
    }

    struct CarDetails
    {
        

        public CarDetails(string carName, string crossingName, DateTime timeOfCrossing)
        {
            CarName = carName;
            CrossingName = crossingName;
            TimeOfCrossing = timeOfCrossing;
        }

        public string CarName { get; set; }
        public string CrossingName { get; set; }

        public DateTime TimeOfCrossing { get; set; }

        public void PrintDetails()
        {
            Console.WriteLine("Car = {0}, Crossing = {1}, Time = {2}", this.CarName, this.CrossingName, this.TimeOfCrossing);
        }
    }

}