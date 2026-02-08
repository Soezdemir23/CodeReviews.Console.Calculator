using CalculationEntry;
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

    }

    public void AddEntry()
    {

    }

    public void ClearHistory();

    public List<CalculationHistory> GetAllEntries() => history;

}