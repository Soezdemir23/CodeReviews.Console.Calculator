// Program.cs
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram
{

    class Program
    {
        static void Main(string[] args)
        {

            bool endApp = false;
            UsageCounter counter = new();
            CalculationHistory history = new();
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine($"Session number: {counter.GetSessions()}");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                // Ask the user to type the second number.
                Console.Write("Type another number, and then press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine($"\th - History [Entries: {history.GetAllEntries().Count}]");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[a|s|m|d|h]"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    try
                    {
                        if (Regex.IsMatch(op, "[h]"))
                        {
                            var currentHistory = history.GetAllEntries();
                            Console.Clear();
                            Console.WriteLine($"Entries: {currentHistory.Count}");
                            System.Console.WriteLine("----------------------------------");
                            foreach (var entry in currentHistory)
                            {
                                System.Console.WriteLine($"{entry.getOperation()}, {entry.getFirstNum()}, {entry.getSecondNum()}, {entry.getResult()}, {entry.getDate()}");
                            }
                            System.Console.WriteLine("-----------------------------------");
                            System.Console.WriteLine("Do you want to delete the history?");
                            System.Console.WriteLine("\ty - Yes");
                            System.Console.WriteLine("\tn - No");
                            string? choice = Console.ReadLine()?.ToLower();
                            if (choice == null || !Regex.IsMatch(choice, "[y|n]"))
                            {
                                System.Console.WriteLine("Error: Unrecognized input");
                            }
                            else if (choice.Equals("y"))
                            {
                                history.ClearHistory();
                                System.Console.WriteLine("History deleted");
                            }
                            else
                            {
                                System.Console.WriteLine("History have not been deleted");
                            }
                        }
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            string operation = String.Empty;

                            switch (op)
                            {
                                case "a":
                                    operation = "Add";
                                    break;
                                case "s":
                                    operation = "Substraction";
                                    break;
                                case "m":
                                    operation = "Multiplication";
                                    break;
                                case "d":
                                    operation = "Division";
                                    break;

                                default:
                                    System.Console.WriteLine("Error in Program.cs trying to create entry (line 106)");
                                    break;
                            }
                            var newEntry = new CalculationEntry(
                                operation,
                                cleanNum1,
                                cleanNum2,
                                result,
                                DateTime.UtcNow
                            );
                            history.AddEntry(newEntry);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        System.Console.WriteLine($"This is the stacktrace: {e.Message}");
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            counter.IncrementSessions();
            counter.SaveSessionsLog();
            history.SaveHistory();
            calculator.FinishLogging();
            return;
        }
    }
}