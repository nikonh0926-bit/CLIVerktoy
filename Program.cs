
public static class Program
{
    public static void Main(string[] args)
    {
        // sjekker om programmet ble startet med kommando-linje argumenter
        if(args.Length > 0)
        {
            Menu.RunSelectedMethod(args[0], args);
        }
        else
        {
            string mode = Menu.GetSelectionFromMenu("Guided mode?");

            // start den interactive guidede menyen
            if (mode == "yes")
            {
                while(true)
                    Menu.GetSelectionFromMenu();
            }
            // start program for tekstkommandoer
            if(mode == "no")
            {
                while(true)
                    Menu.ManualMode();
            }
        }
    }
   
}
    
