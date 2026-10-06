using Spectre.Console;

class FileHandler
{
    static public string path = "Files";
    public static void LS()
    {
      List<string> files = Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories)
      .OrderBy(x => x)
      .ToList();
      foreach (string file in files)
      {
          AnsiConsole.WriteLine(Path.GetFileName(file));
      }
    }
    static List<string> LSReturnList(bool directory = false)
    {
      if(!directory)
        return Directory.GetFiles(path).ToList();
      else
        return Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories)
        .Where(x => Directory.Exists(x))
        .Prepend(path)
        .OrderBy(x => x)
        .ToList();
    }
    public static void CAT()
    {
      string selected = Menu.GetSelectionFromMenu("Select file to read", LSReturnList());
      AnsiConsole.WriteLine(File.ReadAllText(selected));
    }
    public static void ECHO(string argsText = "")
    {
      if(argsText.Length > 0)
        AnsiConsole.WriteLine(argsText);
      else
      {
        string text = AnsiConsole.Ask<string>("Enter text:");
        AnsiConsole.WriteLine(text);
      }
    }
    public static void PWD()
    {
      string changeDirectory = Menu.GetSelectionFromMenu($"Currently in: {path}.\nSwitch?");
      if(changeDirectory == "yes")
      {
        path = Menu.GetSelectionFromMenu("New path", LSReturnList(true));
      }
    }
    public static void HEAD()
    {
      string file = Menu.GetSelectionFromMenu("Select file to read", LSReturnList());
      int lines = Menu.GetSelectionFromMenu("select how many lines from the top to read", File.ReadAllLines(file).Count());
      foreach(string line in File.ReadLines(file).Take(lines))
      {
        AnsiConsole.WriteLine(line);
      } 
    }
    public static void HEAD(string file, int lineCount)
  {
    foreach(string line in File.ReadLines(file).Take(lineCount))
      {
        AnsiConsole.WriteLine(line);
      } 
  }
    public static void TAIL()
    {
      string file = Menu.GetSelectionFromMenu("Select file to read", LSReturnList());
      int lines = Menu.GetSelectionFromMenu("select how many lines from the bottom to read", File.ReadAllLines(file).Count());
      foreach(string line in File.ReadLines(file).TakeLast(lines))
      {
      AnsiConsole.WriteLine(line);
      }
    }
    public static void TAIL(string file, int lineCount)
  {
    foreach(string line in File.ReadLines(file).TakeLast(lineCount))
    {
      AnsiConsole.WriteLine(line);
    }
  }
    public static void WC()
    {
      string file = Menu.GetSelectionFromMenu("select file", LSReturnList());
      AnsiConsole.Write($@"
      Number of words - {WordCounter(file)}
      Number of lines - {File.ReadAllLines(file).Count()}
      Number of characters - {CharCounter(file)}
      Number of bytes - {File.ReadAllBytes(file).Count()}
      ");
    }
    public static void WC(string file)
    {
      AnsiConsole.Write($@"
      Number of words - {WordCounter(file)}
      Number of lines - {File.ReadAllLines(file).Count()}
      Number of characters - {CharCounter(file)}
      Number of bytes - {File.ReadAllBytes(file).Count()}
      ");
    }
    public static void TOUCH()
    {
      AnsiConsole.WriteLine("Enter file name:");
      string fileName = AnsiConsole.Ask<string>("> ");
      File.Create($"{path}\\{fileName}").Close();
      AnsiConsole.WriteLine("File created");
    } 
    public static void TOUCH(string fileName, string path)
  {
    File.Create($"{path}\\{fileName}").Close();
    AnsiConsole.WriteLine("File created");
  }
    public static void CP()
    {
      string sourceFileName = Menu.GetSelectionFromMenu(
        "Select source file",
        LSReturnList()
      );

      string destination = Menu.GetSelectionFromMenu(
        "Select destination",
        LSReturnList(true)
      );

      string name = Path.GetFileNameWithoutExtension(sourceFileName);
      string extension = Path.GetExtension(sourceFileName);

      int number = 0;
      string newName;
      string newDestination;

      do
      {
        newName = number == 0
            ? $"{name}_Copy{extension}"
            : $"{name}_Copy{number}{extension}";

        newDestination = Path.Combine(destination, newName);
        number++;

      } while (File.Exists(newDestination));

      File.Copy(sourceFileName, newDestination);
  }
    public static void CP(string sourceFileName, string destination)
  {
    string name = Path.GetFileNameWithoutExtension(sourceFileName);
    string extension = Path.GetExtension(sourceFileName);

    int number = 0;
    string newName;
    string newDestination;

    do
    {
      newName = number == 0
          ? $"{name}_Copy{extension}"
          : $"{name}_Copy{number}{extension}";

      newDestination = Path.Combine(destination, newName);
      number++;

    } while (File.Exists(newDestination));

    File.Copy(sourceFileName, newDestination);
  }
    public static void MV()
    {
      string sourceFileName = Menu.GetSelectionFromMenu("Select source file:", LSReturnList());
      string destination = Menu.GetSelectionFromMenu("select destination", LSReturnList(true));
      string name = Path.GetFileName(sourceFileName);

      File.Move(sourceFileName, Path.Combine(destination, name));
    }
    public static void MV(string file, string path)
  {
    string name = Path.GetFileName(file);
    File.Move(file, Path.Combine(path, name));
  }
    public static void RM()
    {
      string selected = Menu.GetSelectionFromMenu("Select file to delete:", LSReturnList());
      File.Delete(selected);
      AnsiConsole.WriteLine("File deleted");
    }
    public static void RM(string file)
    {
      File.Delete(file);
      AnsiConsole.WriteLine("File deleted");
    }
    static int WordCounter(string selectedFile)
  {
    var file = File.ReadAllLines(selectedFile);
    int wordCount = 0;
    foreach(string s in file)
    {
      string[] lines = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
      wordCount += lines.Count();
    }
    return wordCount;
  }
    static int CharCounter(string selectedFile)
  {
    var file = File.ReadAllLines(selectedFile);
    int charCount = 0;
    foreach(string s in file)
    {
      charCount += s.ToCharArray().Count();
    }
    return charCount;
  }
}