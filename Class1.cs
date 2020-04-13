class EventPublisher {
    public delegate void EventHandler(object sender, object eventData);
    public event EventHandler customEvent;

    public void publishEvent() {
        customEvent(sender, 5);
    }

}

class EventListener {

    public void eventOccured(object sender, object eventData) {
        System.Console.WriteLine("Event Occured"+eventData.ToString());
    }

    public static void Main(string[] args) {
        EventPublisher publisher = new EventPublisher();
        publisher.customEvent += new EventPublisher.EventHandler(eventOccured);
        publisher.publishEvent();

    }

}
