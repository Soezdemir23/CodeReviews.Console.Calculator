namespace CalculatorLibrary;

public class UsageCounter
{
    // there should be a file to read for the counter.
    // A Trace for logging would be interesting, 
    // wherein the number of sessions do not change.

    private int sessions;

    public UsageCounter()
    {
        if (!File.Exists(Directory.GetCurrentDirectory()))
        {
            File.CreateText("UsageCounter.log");
        }
        // then, access the logfile, simple. Should also only be a line

        // try to parse it into a number, if it doesn't 
        // work simply call an exception with the attempt not working.

        // assign it to the variable sessions.

    }

    public int GetSessions()
    {
        return sessions;
    }

    public void IncrementSessions()
    {
        sessions++;
    }

    public void SaveSessions()
    {
        // since we are simply incrementing the number of sessions,
        // we can set the incremented session here into the log file
        // later.
    }
}