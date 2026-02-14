namespace CalculatorLibrary;

public class UsageCounter
{
    // there should be a file to read for the counter.
    // A Trace for logging would be interesting, 
    // wherein the number of sessions do not change.


    private int sessions;
    private string content = string.Empty;
    private readonly string path = Directory.GetCurrentDirectory() + "/usagecounter.log";

    public UsageCounter()
    {
        LoadSessionsLog();
    }

    public int GetSessions()
    {
        return sessions;
    }

    public void IncrementSessions()
    {
        sessions++;
    }

    public void LoadSessionsLog()
    {
        Console.WriteLine(path);
        // check if the file even exists 
        // where it is supposed to be (root where sln is)
        if (File.Exists(path))
        {
            // try reading the file, try parsing it to int, exists = true
            try
            {
                content = File.ReadAllText(path);
                if (string.IsNullOrEmpty(content))
                {
                    sessions = 0;
                }
                else
                {
                    sessions = int.Parse(content);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in UsageCounter.LoadSessionsLog:\nSomething went wrong accessing/reading the file: " + e.Message);
                System.Console.WriteLine("Do you want to still continue? Sessions might not be logged");
                System.Console.WriteLine("Enter [Y/y] to continue or [N/n] to exit:");
                while (true)
                {
                    // coalescence into an empty string, 
                    // for an example if the process is aborted midway
                    string? prompt = Console.ReadLine()?.ToLower() ?? string.Empty;

                    if (prompt.Equals("n"))
                    {
                        System.Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                    }
                    else if (prompt.Equals("y"))
                    {
                        System.Console.WriteLine("Continuing erroneous program");
                        break;
                    }
                    System.Console.WriteLine("Enter [Y/y] to continue or [N/n] to exit:");
                }
            }
        }
        // File doesn't exist where we wanted to find it. 
        // So we create it. Remember path is also containing where the file is:
        else
        {
            File.CreateText(path);
            sessions = 0;
        }
    }
    public void SaveSessionsLog()
    {
        //attempt saving:
        try
        {
            File.WriteAllTextAsync(path, sessions.ToString());
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error in UsageCounter.SaveSessionsLog: " + e.Message);
            System.Console.WriteLine($"Exception at: {e.Message}");
        }
    }
}