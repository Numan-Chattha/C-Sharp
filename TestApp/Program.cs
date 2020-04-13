using Random = System.Random;
public class EventClass
{
    // Declare the delegate type: 
    public delegate void CustomEventHandler(object sender, int number);

    // Declare the event variable using the delegate type: 
    public event CustomEventHandler CustomEvent;

    public void InvokeEvent()
    {
        // Invoke the event from within the class that declared the event:
        
        Random r = new Random();
        int res;
        for (int i = 0; i < 1000; i++)
        {
           res = r.Next(0, 100);
            if (res % 15 == 0)
            {
                System.Console.WriteLine("Number {0} is multiple of 15 Generating event",res);
                CustomEvent(this, res);
                break;
            }
        }
    }
}

class TestEvents
{
    private static void CodeToRun(object sender, int number)
    {
        System.Console.WriteLine("CodeToRun is executing number = "+ number);
    }

    private static void MoreCodeToRun(object sender, int number)
    {
        System.Console.WriteLine("MoreCodeToRun is executing number = " + number);
    }

    static void Maina()
    {
        EventClass ec = new EventClass();

        ec.CustomEvent += new EventClass.CustomEventHandler(CodeToRun);
        ec.CustomEvent += new EventClass.CustomEventHandler(MoreCodeToRun);

        System.Console.WriteLine("First Invocation:");
        ec.InvokeEvent();

        ec.CustomEvent -= new EventClass.CustomEventHandler(MoreCodeToRun);

        System.Console.WriteLine("\nSecond Invocation:");
        ec.InvokeEvent();
    }
}