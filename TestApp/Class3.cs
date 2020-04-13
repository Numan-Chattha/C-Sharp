using System;
using Random = System.Random;
class EventPublisher
{

    public delegate void EventHandler(object sender, object eventData);
    public event EventHandler CustomEvent;
    public int NumberToLook4
    {
        get;
        set;
    }
    public EventPublisher(int NumberToLook4)
    {
        this.NumberToLook4 = NumberToLook4;
    }
    public void publishEvent()
    {
        Random r = new Random();
        int res;
        for (int i = 0; i < 1000; i++)
        {
            res = r.Next(1, 100);
            if (res % NumberToLook4 == 0)
            {
                CustomEvent(this, res);
                //break;
            }
        }
    }

}

class EventListener1
{
    static int input;
    private static void eventOccured(object sender, object eventData)
    {
        System.Console.WriteLine("Number {0} is multiple of given number", eventData);
    }

    public static void Main1(string[] args)
    {
        try
        {
            Console.WriteLine("Input Number:");
            input = Convert.ToInt32(Console.ReadLine());
        }
        catch (System.Exception)
        {
            throw;
        }
        EventPublisher publisher = new EventPublisher(input);
        publisher.CustomEvent += new EventPublisher.EventHandler(eventOccured);
        publisher.publishEvent();

    }

}
