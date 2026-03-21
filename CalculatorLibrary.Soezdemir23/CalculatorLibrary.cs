// CalculatorLibrary.cs
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CalculatorLibrary;

public class Calculator : IDisposable
{
    JsonWriter writer;
    //recommendation from Copilot to properly dispose the JSON logging
    private bool disposed;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile)
        {
            Formatting = Formatting.Indented
        };
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }

    public double DoOperation(double num1, double num2, string op)
    {
        // if the file is not disposed, 
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(Calculator));
        }
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");

                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;

            case "r":
                result = Math.Sqrt(num1);
                writer.WriteValue("Square root of first Number");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power function");
                break;
            case "x":
                result = Math.Pow(10, num1);
                writer.WriteValue("Power of Ten");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WriteEndObject();


        return result;
    }
    public void Dispose()
    {
        if (disposed) return;

        // Close JSON structure before disposing writer.
        // review from copilot
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();

        disposed = true;
        GC.SuppressFinalize(this);
    }
}