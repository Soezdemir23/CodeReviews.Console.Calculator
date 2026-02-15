namespace CalculatorLibrary;

public class CalculationHistory
{
    readonly List<CalculationEntry> history = new();
    readonly string path = Path.Combine(Directory.GetCurrentDirectory(), "calculationHistory.log");

    public CalculationHistory()
    {
        LoadHistory();
    }
    public void LoadHistory()
    {
        try
        {
            if (File.Exists(path))
            {
                string[] content = File.ReadAllLines(path);
                if (content.Length > 0)
                {
                    foreach (var row in content)
                    {
                        string[] columns = row.Split(",");
                        history.Add(new CalculationEntry(
                            columns[0],
                            double.Parse(columns[1]),
                            double.Parse(columns[2]),
                            double.Parse(columns[3]),
                            DateTime.Parse(columns[4])
                        )
                        );
                    }
                }
            }
            else
            {
                // if file is not existing in path, create it,
                // dispose the returned Streamwrite object
                File.Create(path).Dispose();
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine("Error in CalculationHistory.LoadHistory:\n" + e.Message);
        }
    }

    public void SaveHistory()
    {

        List<string> historyConverted = new();
        foreach (var entry in history)
        {
            string row =
                $"{entry.Operation},{entry.FirstNum},{entry.SecondNum},{entry.Result},{entry.Date}";
            historyConverted.Add(row);
        }
        File.WriteAllLines(path, historyConverted);
    }

    public void AddEntry(CalculationEntry entry)
    {
        history.Add(entry);
    }

    public CalculationEntry GetEntryByID(int i) => history.ElementAt(i);

    public void ClearHistory()
    {
        // create a new file, close the stream immediately afterwards.
        File.WriteAllText(path, string.Empty);
        history.Clear();
    }

    public List<CalculationEntry> GetAllEntries() => history;

}