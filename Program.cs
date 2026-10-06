
public static class Program
{
    public static void Main(string[] args)
    {
        if(args.Length > 0)
        {
            Menu.RunSelectedMethod(args[0], args);
        }
        else
        {
            string mode = Menu.GetSelectionFromMenu("Guided mode?");
            while(mode == "yes")
            {
                Menu.GetSelectionFromMenu();
            }
            while(mode == "no")
            {
                Menu.ManualMode();
            }
        }
    }
   
}
    
