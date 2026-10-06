using Spectre.Console;
class Menu
{
    public static void ManualMode()
    {
        var selected = AnsiConsole.Ask<string>($"{FileHandler.path} ");
        string[] queue = selected.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        RunSelectedMethod(queue[0], queue);
    }
    public static void GetSelectionFromMenu()
  {
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select options:")
        .AddChoices("ls", "cat", "echo", "pwd", "head", "tail", "wc", "touch", "cp", "mv", "rm", "exit")
    );
    RunSelectedMethod(selected);
  }
  public static void RunSelectedMethod(string selected, string[]? args = null)
    {
        switch (selected)
    {
        case "ls":
            FileHandler.LS();
            break;
        case "cat":
            FileHandler.CAT();
            break;
        case "echo":
            if(args != null && args.Length > 1)
                {
                    string text = string.Join(" ", args.Skip(1));
                    FileHandler.ECHO(text);
                }
                else
                    FileHandler.ECHO();
            break;
        case "pwd":
            FileHandler.PWD();
            break;
        case "head":
            if(args != null && args.Length > 2)
            {
                if(!File.Exists(args[1]))
                {
                    AnsiConsole.WriteLine("File not found");
                    break;
                }
                else if(!int.TryParse(args[2], out int line))
                {
                    AnsiConsole.WriteLine("Line Count not valid");
                    break;
                }
                else
                    FileHandler.HEAD(args[1], line);    
            }
            else
                FileHandler.HEAD();
            break;
        case "tail":
            if(args != null && args.Length > 2)
            {
                if(!File.Exists(args[1]))
                {
                    AnsiConsole.WriteLine("File not found");
                    break;
                }
                else if(!int.TryParse(args[2], out int line))
                {
                    AnsiConsole.WriteLine("Line count not valid");
                    break;
                }
                else
                FileHandler.TAIL(args[1], line);
            }
            else
                FileHandler.TAIL();
            break;
        case "wc":
            if(args != null && args.Length > 1)
            {
                if(!File.Exists(args[1]))
                {
                    AnsiConsole.WriteLine("File not found");
                    break;
                }
                else
                FileHandler.WC(args[1]);
            }
            else
                FileHandler.WC();
            break;
        case "touch":
            if(args != null && args.Length > 2)
            {
                if(File.Exists(args[1]))
                    AnsiConsole.WriteLine("File name already exists");
                else if (!Path.Exists(args[2]))
                    AnsiConsole.WriteLine("File path does not exist");
                
                else
                    FileHandler.TOUCH(args[1], args[2]);
            }
            else
                FileHandler.TOUCH();
            break;
        case "cp":
            if(args != null && args.Length > 2)
            {
                if(!File.Exists(args[1]))
                    AnsiConsole.WriteLine("File name does not exist");
                else if (!Path.Exists(args[2]))
                    AnsiConsole.WriteLine("File path does not exist");
                else
                    FileHandler.CP(args[1], args[2]);
            }
            else
            FileHandler.CP();
            break;
        case "mv":
            if(args != null && args.Length > 2)
            {
                if(!File.Exists(args[1]))
                    AnsiConsole.WriteLine("File name does not exist");
                else if (!Path.Exists(args[2]))
                    AnsiConsole.WriteLine("File path does not exist");
                else
                    FileHandler.MV(args[1], args[2]);
            }
            else
                FileHandler.MV();
            break;
        case "rm":
            if(args != null && args.Length > 1)
                {
                    if(!File.Exists(args[1]))
                        AnsiConsole.WriteLine("File not found");
                    else
                        FileHandler.RM(args[1]);
                }
            else
                FileHandler.RM();
            break;
        case "exit":
            Environment.Exit(0);
            break;
        default:
            AnsiConsole.WriteLine("no valid command found");
            break;
      }
    }
  public static string GetSelectionFromMenu(string title, List<string> options)
  {
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title(title)
        .AddChoices(options)
    );
    return selected;
  }
  public static string GetSelectionFromMenu(string title)
  {
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title(title)
        .AddChoices("yes", "no")
    );
    return selected;
  }
  public static int GetSelectionFromMenu(string title, int lines)
  {
    List<int> numbers = Enumerable.Range(1, lines).ToList();
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<int>()
        .Title(title)
        .AddChoices(numbers)
    );
    return selected;
  }
}