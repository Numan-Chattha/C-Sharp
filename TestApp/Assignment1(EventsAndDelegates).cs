using System;
using System.Threading;

namespace Assignment1
{


    class TrainSignal {

        public delegate void TrainSignalEventHandler(object sender, string eventData);
        public event TrainSignalEventHandler SignalChanged;
        public string Name { get; set; }
        public TrainSignal(string name) {
            this.Name = name;
        }
        internal virtual void OnSignalChanged(string eventDetails,string train) {
            Console.WriteLine("{0}({1}-{2})",eventDetails,this.Name,train);
            SignalChanged?.Invoke(this, eventDetails);
        }

    }

    class Car
    {
        private readonly string name;
        private TrainSignal _signal;

        public Car(TrainSignal signal,string name) {
            this._signal = signal;
            signal.SignalChanged += SignalChanged;
            this.name = name;
        }

        public TrainSignal Signal {
            get {
                return _signal;
            }
            set {
                this._signal = value;
                this._signal.SignalChanged += SignalChanged;
            }
        }
        public  void SignalChanged(object sender, string eventData)
        {
            /*Console.WriteLine("Signal Changed.");*/
            if (eventData == EventListener.INCONMMING)
            {
                this.stop();
            }
            else {
                this.Start(); 
                this._signal.SignalChanged -= SignalChanged;
            }
        }
        private void stop() {
            Console.WriteLine("Stopping {0}",name);
        }
        private void Start()
        {
            Console.WriteLine("Moving {0}", name);
        }
    }

    class EventListener {

        const string Train_Incomming = "TRAIN_INCONMMING";
        const string Track_Clear = "TRACK_CLEAR";

        public static string INCONMMING { get { return Train_Incomming; } }
        public string CLEAR { get { return Track_Clear; } }

        public static void Main(string[] args)
        {
            const string TRAIN_1 = "train#1";
            const string TRAIN_2 = "train#2";
            TrainSignal railwayCrossing1 = new TrainSignal("Crossing#1");
            TrainSignal railwayCrossing2 = new TrainSignal("Crossing#2");
            Car car1 = new Car(railwayCrossing1,"car1");
            Car car2 = new Car(railwayCrossing1, "car2");
            Car car3 = new Car(railwayCrossing2, "car3");
            //Incomming Train1 at crossing#1
            railwayCrossing1.OnSignalChanged(INCONMMING, TRAIN_1);
            Thread.Sleep(1000);
            //Incomming Train2 at crossing#2
            railwayCrossing2.OnSignalChanged(INCONMMING, TRAIN_2);
            Thread.Sleep(3000);
            //Train2 Crossed crossing#2 Clear
            railwayCrossing2.OnSignalChanged(Track_Clear, TRAIN_2);
            Thread.Sleep(1000);
            //Train1 Crossed crossing#1 Clear
            railwayCrossing1.OnSignalChanged(Track_Clear, TRAIN_1);




            railwayCrossing2.OnSignalChanged(INCONMMING, TRAIN_1);
            railwayCrossing2.OnSignalChanged(Track_Clear, TRAIN_1);
            car3.Signal = railwayCrossing1;

            Thread.Sleep(1000);
            railwayCrossing1.OnSignalChanged(INCONMMING, TRAIN_2);
            Thread.Sleep(3000);
            //Train2 Crossed crossing#1 Clear
            railwayCrossing1.OnSignalChanged(Track_Clear, TRAIN_2);

        }

    }
}
