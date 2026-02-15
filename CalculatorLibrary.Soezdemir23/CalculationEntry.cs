namespace CalculatorLibrary;

public class CalculationEntry
{


    public string Operation { get; set; }
    public double FirstNum { get; set; }
    public double SecondNum { get; set; }
    public double Result { get; set; }
    public DateTime Date { get; set; }

    public CalculationEntry(
        string operation, double firstNum,
        double secondNum, double result, DateTime date)
    {
        Operation = operation;
        FirstNum = firstNum;
        SecondNum = secondNum;
        Result = result;
        Date = date;

    }

}