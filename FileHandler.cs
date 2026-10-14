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
          if(Directory.Exists(file))
            AnsiConsole.WriteLine(Path.GetFileName(file) + "\\");
          else
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
    static bool FindDirectory(string path, out string newPath)
  {
      List<string> directories = LSReturnList(true);
      foreach(string f in directories)
    {
      if(Path.GetFileName(f) == path)
      {
        newPath = f;
        return true;
      }
    }
    newPath = "not found";
    return false;
  }
    public static void CAT()
    {
      var file = Menu.GetSelectionFromMenu("Select file to read", LSReturnList());
      AnsiConsole.WriteLine(File.ReadAllText(file));
    }
    public static void CAT(string file, string path)
    {
      string filePath = Path.Combine(path, file);
      if(File.Exists(filePath))
        AnsiConsole.WriteLine(File.ReadAllText(filePath));
      else
        AnsiConsole.WriteLine("File not found");
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
    public static void PWD(string newPath)
  {
    if(FindDirectory(newPath, out string correctedPath))
      path = correctedPath;
    else
      AnsiConsole.Write("path not found");
  }
    public static void HEAD()
    {
      string file = Menu.GetSelectionFromMenu("Select file to read", LSReturnList());

      int lines = Menu.GetSelectionFromMenu("select how many lines from the top to read", File.ReadAllLines(file).Count());
      
      if (!HEAD(file, lines, Path.GetFileName(file)!, out string error))
      {
        AnsiConsole.WriteLine(error);
      }
    }
    public static bool HEAD(string file, int lineCount, string path, out string errorMessage)
  {
      string correctedFile = Path.Combine(path, file);

      if(!File.Exists(correctedFile))
      {
        errorMessage = "File not found";
        return false;
      }

      string[] lines = File.ReadAllLines(correctedFile);
      if(lineCount < 1)
      {
        errorMessage = "linecount can not be less than 1";
        return false;
      }
      else if(lines.Length < lineCount)
      {
        AnsiConsole.WriteLine("Line count exceeding lines in file, printing all lines");
        foreach(string line in lines)
        {
          AnsiConsole.WriteLine(line);
        }
        errorMessage = "";
        return true;
      }
      foreach(string line in lines.Take(lineCount))
      {
        AnsiConsole.WriteLine(line);
      } 
      errorMessage = "";
      return true;
  }
    public static void TAIL()
    {
      string file = Menu.GetSelectionFromMenu("Select file to read", LSReturnList());
      int lines = Menu.GetSelectionFromMenu("select how many lines from the bottom to read", File.ReadAllLines(file).Count());
      
      if (!TAIL(file, lines, Path.GetFileName(file)!, out string error))
      {
        AnsiConsole.WriteLine(error);
      }
    }
    public static bool TAIL(string file, int lineCount, string path, out string errorMessage)
  {
    string correctedFile = Path.Combine(path, file);

    if(!File.Exists(correctedFile))
    {
      errorMessage = "File not found";
      return false;
    }

    string[] lines = File.ReadAllLines(correctedFile);

    if(lineCount < 1)
    {
      errorMessage = "linecount can not be less than 1";
      return false;
    }
    else if(lineCount > lines.Length)
    {
      AnsiConsole.WriteLine("linecount exceeds number of lines in file. Printing all lines");
      foreach(string line in lines)
        AnsiConsole.WriteLine(line);
      errorMessage = "";
      return true;
    }

    foreach(string line in lines.TakeLast(lineCount))
      AnsiConsole.WriteLine(line);
    errorMessage = "";
    return true;
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
    public static bool WC(string file, string path, out string error)
    {
      string correctedFile = Path.Combine(path, file);

      if(!File.Exists(correctedFile))
      {
      error = "file not found";
      return false;
      }
      // charCounter teller ikke linjeskift
      AnsiConsole.Write($@"
      Number of words - {WordCounter(correctedFile)}
      Number of lines - {File.ReadAllLines(correctedFile).Count()}
      Number of characters - {CharCounter(correctedFile)}
      Number of bytes - {File.ReadAllBytes(correctedFile).Count()}
      ");
      error = "";
      return true;
    }
    public static void TOUCH()
    {
      AnsiConsole.WriteLine("Enter file name:");
      string fileName = AnsiConsole.Ask<string>("> ");
      File.Create(Path.Combine(path, fileName)).Close();
      AnsiConsole.WriteLine("File created");
    } 
    public static bool TOUCH(string fileName, string path, out string error)
  {
    string correctedFile = Path.Combine(path, fileName);

    if(File.Exists(correctedFile))
    {
      error = "File already exists";
      return false;
    }
    else if (!Directory.Exists(path))
    {
      error = "Path does not exist";
      return false;
    }
    File.Create(correctedFile).Close();
    AnsiConsole.WriteLine("File created");
    error = "";
    return true;
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
    public static bool CP(string file, string path, out string error)
  {
    if(!File.Exists(file))
    {
      error = "File does not exist";
      return false;
    }
    else if(!Directory.Exists(path))
    {
      error = "Given path does not exist";
      return false;
    }
    int number = 0;
    string newName;
    string newDestination;

    string extension = Path.GetExtension(file);
    string name = Path.GetFileNameWithoutExtension(file);

    do
    {
      newName = number == 0
          ? $"{name}_Copy{extension}"
          : $"{name}_Copy{number}{extension}";

      newDestination = Path.Combine(path, newName);
      number++;

    } while (File.Exists(newDestination));

    File.Copy(file, newDestination);
    error = "";
    return true;
  }
    public static void MV()
    {
      string sourceFileName = Menu.GetSelectionFromMenu("Select source file:", LSReturnList());
      string destination = Menu.GetSelectionFromMenu("select destination", LSReturnList(true));
      string name = Path.GetFileName(sourceFileName);

      File.Move(sourceFileName, Path.Combine(destination, name));
    }
    public static bool MV(string file, string path, out string error)
  {
    if(!File.Exists(file))
    {
      error = "File does not exist";
      return false;
    }
    else if(!Directory.Exists(path))
    {
      error = "Path does not exist";
      return false;
    }

    string name = Path.GetFileName(file);
    File.Move(file, Path.Combine(path, name));
    error = "";
    return true;
  }
    public static void RM()
    {
      string selected = Menu.GetSelectionFromMenu("Select file to delete:", LSReturnList());
      File.Delete(selected);
      AnsiConsole.WriteLine("File deleted");
    }
    public static bool RM(string file, out string error)
    {
      if(!File.Exists(file))
      {
        error = "File does not exist";
        return false;
      }

      File.Delete(file);
      AnsiConsole.WriteLine("File deleted");
      error = "";
      return true;
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