public enum KarakterState { Berdiri, Terbang, Jongkok, Tengkurap};

public enum Trigger { TombolW, TombolS, TombolX};

class PosisiKarakterGame
{
    public KarakterState prevState;
    public KarakterState nextState;
    public KarakterState currentState;
    public Trigger trigger;

    public PosisiKarakterGame(KarakterState prevState, KarakterState nextState, Trigger trigger)
    {
        this.prevState = prevState;
        this.nextState = nextState;
        this.trigger = trigger;
    }

    private static PosisiKarakterGame[] transitions =
    {
        new PosisiKarakterGame(KarakterState.Berdiri, KarakterState.Terbang, Trigger.TombolW),
        new PosisiKarakterGame(KarakterState.Berdiri, KarakterState.Jongkok, Trigger.TombolS),
        new PosisiKarakterGame(KarakterState.Terbang, KarakterState.Berdiri, Trigger.TombolS),
        new PosisiKarakterGame(KarakterState.Terbang, KarakterState.Jongkok, Trigger.TombolX),
        new PosisiKarakterGame(KarakterState.Jongkok, KarakterState.Berdiri, Trigger.TombolW),
        new PosisiKarakterGame(KarakterState.Jongkok, KarakterState.Tengkurap, Trigger.TombolS),
        new PosisiKarakterGame(KarakterState.Tengkurap, KarakterState.Jongkok, Trigger.TombolW),
    };

    public KarakterState getNextState(KarakterState prevState, Trigger trigger)
    {
        KarakterState nextState = prevState;

        for (int i = 0; i < transitions.Length; i++)
        {
            if (transitions[i].prevState == prevState && transitions[i].trigger == trigger)
            {
                nextState = transitions[i].nextState;
            }
        }
        return nextState;
    }

    public void activeTrigger(Trigger trigger)
    {
        KarakterState nextState = getNextState(currentState, trigger);
        this.currentState = nextState;

        if (trigger == Trigger.TombolS)
        {
            Console.WriteLine("tombol arah bawah ditekan");
        }
        if (trigger == Trigger.TombolW)
        {
            Console.WriteLine("tombol arah atas ditekan");
        }
    }
}

public class main
{
    public static void Main(string[] args)
    {
        PosisiKarakterGame karakter = new PosisiKarakterGame(KarakterState.Berdiri, KarakterState.Berdiri, Trigger.TombolS);
        karakter.currentState = KarakterState.Berdiri;

        Console.WriteLine($"Kondisi karakter saat ini {Enum.GetName(typeof(KarakterState), karakter.currentState)}");
        Console.WriteLine("Command yang tersedia TombolW, TombolS, TombolX, EXIT");
        Console.Write("Pilih command: ");
        String command = Console.ReadLine();

        while (command != "EXIT")
        {
            if (Enum.TryParse<Trigger>(command, out Trigger trigger))
            {
                karakter.activeTrigger(trigger);
            }
            else
            {
                Console.WriteLine("Command tidak valid");
            }
            Console.Write("Pilih command: ");
            command = Console.ReadLine();
        }

    }
}