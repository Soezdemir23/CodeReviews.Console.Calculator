namespace CalculatorLibrary;

public class CalculationEntry
{

    private string operation;
    private double firstNum;
    private double secondNum;
    private double result;
    private DateTime date;

    public CalculationEntry(
        string operation, double firstNum,
        double secondNum, double result, DateTime date)
    {
        this.operation = operation;
        this.firstNum = firstNum;
        this.secondNum = secondNum;
        this.result = result;
        this.date = date;

    }


    public string getOperation() => operation;
    public void setOperation(string operation) => this.operation = operation;

    public double getFirstNum() => firstNum;
    public void setFirstNum(double firstNum) => this.firstNum = firstNum;

    public double getSecondNum() => secondNum;
    public void setSecondNum(double secondNum) => this.secondNum = secondNum;

    public double getResult() => result;
    public void setResult(double result) => this.result = result;

    public DateTime getDate() => date;
    public void setDate(DateTime date) => this.date = date;
}