namespace CalculatorLibrary;

public class CalculationHistory
{
    List<CalculationEntry> history;
    string path = Directory.GetCurrentDirectory() + "/calculationHistory.log";
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
                File.CreateText(path);
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
                $"{entry.getOperation()},{entry.getFirstNum()},{entry.getSecondNum()},{entry.getResult()},{entry.getDate()}";
            historyConverted.Add(row);
            File.WriteAllLines(path, historyConverted);
        }
    }

    public void AddEntry(CalculationEntry entry)
    {
        history.Add(new CalculationEntry(
            operation: entry.getOperation(),
            firstNum: entry.getFirstNum(),
            secondNum: entry.getSecondNum(),
            result: entry.getResult(),
            date: entry.getDate()
        ));
    }

    public void ClearHistory()
    {
        File.CreateText(path);
        history.Clear();
    }

    public List<CalculationEntry> GetAllEntries() => history;

}