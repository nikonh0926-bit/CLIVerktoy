using Spectre.Console;
class Menu
{
    // hent tekstkommandoer fra konsollen
    public static void ManualMode()
    {
        var selected = AnsiConsole.Ask<string>($"\n{FileHandler.path} ");
        string[] queue = selected.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        if(queue.Length == 0)
            return;
        RunSelectedMethod(queue[0], queue);
    }

    // få kommando fra konsoll i guided meny
    public static void GetSelectionFromMenu()
  {
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("Select options:")
        .AddChoices("ls", "cat", "pwd", "head", "tail", "wc", "touch", "cp", "mv", "rm", "exit")
    );
    RunSelectedMethod(selected);
  }

  // kjør valgt metode.
  // tar imot en kommando og eventuelle argumenter
  public static void RunSelectedMethod(string selected, string[]? userInput = null)
    {
        switch (selected)
    {
        // List opp alle filer og mapper
        case "ls":
            FileHandler.LS();
            break;
        // Les valgt fil
        case "cat":
                if(userInput != null && userInput.Length > 1)
                {
                    FileHandler.CAT(userInput[1], FileHandler.path);
                }
                else
                {
                    FileHandler.CAT();
                }
            break;
        // Echo tekst fra bruker (ikke tilgjengelig i meny-en)
        case "echo":
                if(userInput != null && userInput.Length > 1)
                {
                    string text = string.Join(" ", userInput.Skip(1));
                    FileHandler.ECHO(text);
                }
            break;
        // Bytt nåværende mappe
        case "pwd":
                if (userInput != null && userInput.Length > 1)
                {
                    FileHandler.PWD(userInput[1]);
                }
                else
                {
                    FileHandler.PWD();        
                }
            
            break;
        // Les et antall linjer fra toppen av en fil
        case "head":
            if(userInput != null && userInput.Length > 2)
            {
                if(!int.TryParse(userInput[2], out int line))
                    AnsiConsole.WriteLine("line count not valid");
                else if (!FileHandler.HEAD(userInput[1], line, FileHandler.path, out string error))
                    AnsiConsole.WriteLine(error);   
            }
            else
                FileHandler.HEAD();
            break;
        // Les et antall linjer fra bunnen av en fil
        case "tail":
            if(userInput != null && userInput.Length > 2)
            {
                if (!int.TryParse(userInput[2], out int line))
                    AnsiConsole.WriteLine("Line count not valid");
                else if(!FileHandler.TAIL(userInput[1], line, FileHandler.path, out string error))
                    AnsiConsole.WriteLine(error);
            }
            else
                FileHandler.TAIL();
            break;
        // Skriv opp teknisk informasjon til en fil
        case "wc":
            if(userInput != null && userInput.Length > 1)
            {
                
                if(!FileHandler.WC(userInput[1], FileHandler.path, out string error))
                    AnsiConsole.WriteLine(error);
            }
            else
                FileHandler.WC();
            break;
        // lag en ny fil
        case "touch":
            if(userInput != null && userInput.Length > 2)
            {
                if(!FileHandler.TOUCH(userInput[1], userInput[2], out string error))
                    AnsiConsole.WriteLine(error);
            }
            else
                FileHandler.TOUCH();
            break;
        // kopier en eksisterende fil til en bestemt mappe
        case "cp":
            if(userInput != null && userInput.Length > 2)
            {
                if(!FileHandler.CP(userInput[1], userInput[2], out string error))
                    AnsiConsole.WriteLine(error);
            }
            else
            FileHandler.CP();
            break;
        // flytt en fil fra en mappe til en annen
        case "mv":
            if(userInput != null && userInput.Length > 2)
            {
                if(!FileHandler.MV(userInput[1], userInput[2], out string error))
                    AnsiConsole.WriteLine(error);
            }
            else
                FileHandler.MV();
            break;
        // fjern en fil
        case "rm":
            if(userInput != null && userInput.Length > 1)
                {
                    if(!FileHandler.RM(userInput[1], out string error))
                        AnsiConsole.WriteLine(error);
                }
            else
                FileHandler.RM();
            break;
        // slutt program
        case "exit":
            Environment.Exit(0);
            break;
        // hjelpefunksjon for tekstbasert operasjon
        case "help":
        AnsiConsole.Write(
@"ls - lists all directory and files
cat <file-name> - reads the given file
echo <text> - Prints the given text
pwd <path> - switches to the given path
head <file-name> <lines to read> - reads the given number of lines from the top of the given file
tail <file-name> <lines to read> - reads the given number of lines from the bottom of the given file
wc <file-name> - gives the technical data of the file (wordcount, linecount, charactercount and bytes)
touch <new-file-name> <path> - creates a new file with the given name in the given path
cp <path\file-name> <new path> - creates a copy of the given file and places it in the given directory (adds _copy..._copy1... to the end of the filename)
mv <path\file-name> <new path> - moves the given file to the given directory
rm <file-name> - removes the given file
exit - exits program
"
        );
            break;
        default:
            AnsiConsole.WriteLine("no valid command found");
            break;
      }
    }

    // få kommando fra meny basert på en query liste
  public static string GetSelectionFromMenu(string title, List<string> options)
  {
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title(title)
        .AddChoices(options)
    );
    return selected;
  }

  // få ja eller nei fra konsollen
  public static string GetSelectionFromMenu(string title)
  {
    var selected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title(title)
        .AddChoices("yes", "no")
    );
    return selected;
  }

  // få et nummer fra konsollen basert på antall linjer i et valgt dokument
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