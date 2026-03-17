// Program.cs
using System.Text.RegularExpressions;
using CalculatorLibrary;
using Microsoft.VisualBasic;

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
            Console.WriteLine($"Number of entries in history: {history.GetAllEntries().Count}");
            Console.WriteLine("Press [H/h]istory to view the history of calculations. Any other key to continue");
            Console.WriteLine("------------------------\n");

            string? callhistory = Console.ReadLine()?.ToLower();
            if (callhistory != null && Regex.IsMatch(callhistory, "[h]"))
            {
                Console.WriteLine("\n----------- Calculation History ----------");
                Console.WriteLine($"{"Operation",-15} {"First Number",-15} {"Second Number",-15} {"Result",-15} {"Date",-15}");
                System.Console.WriteLine(new string('-', 85));
                foreach (var entry in history.GetAllEntries())
                {
                    Console.WriteLine($"{entry.Operation,-15} {entry.FirstNum,-15:F4} {entry.SecondNum,-15:F4} {entry.Result,-15:F4} {entry.Date,-20}");
                }
                Console.WriteLine("Do you want to delete the history?");
                Console.WriteLine("\ty - Yes");
                Console.WriteLine("\tany other key - No");
                string? choice = Console.ReadLine()?.ToLower();
                if (choice != null && Regex.IsMatch(choice, "[y]"))
                {
                    history.ClearHistory();
                    Console.WriteLine("History has been deleted... press any key to proceed to calculation");
                }
                else if (choice != null && Regex.IsMatch(choice, "[n]"))
                {
                    Console.WriteLine("proceeding to calculation... press any key");
                }
                else
                {
                    System.Console.WriteLine("entered different command, proceeding to the calculator");
                }
                Console.ReadKey();
            }

            Calculator calculator = new Calculator();
            bool skipSessionCount = false;

            while (!endApp)
            {
                // TODO: remove this variable if it has no place in solving the issue:
                // choosing the json
                Console.WriteLine("");

                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                double result = 0;
                double cleanNum1 = OperationNumber(history, true);
                double cleanNum2 = OperationNumber(history, false);

                // Ask the user to choose an operator.
                // TODO: - [x] sqrt
                //       - [x] Taking the power
                //       - [x] 10x
                //       - [ ] Trigonometry functions.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine($"\tr - Square root of {cleanNum1}");
                System.Console.WriteLine("\tp - Power function");
                System.Console.WriteLine("\tx - Power of 10");


                Console.WriteLine($"\th - History [Entries: {history.GetAllEntries().Count}]");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "[asmdhrpx]"))
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
                            Console.WriteLine("----------------------------------");
                            Console.WriteLine("\n----------- Calcluation History ----------");
                            Console.WriteLine($"{"Operation",-15} {"First Number",-15} {"Second Number",-15} {"Result",-15} {"Date",-15}");
                            System.Console.WriteLine(new string('-', 85));
                            foreach (var entry in history.GetAllEntries())
                            {
                                Console.WriteLine($"{entry.Operation,-15} {entry.FirstNum,-15:F4} {entry.SecondNum,-15:F4} {entry.Result,-15:F4} {entry.Date,-20}");
                            }
                            Console.WriteLine("Do you want to delete the history?");
                            Console.WriteLine("\ty - Yes");
                            Console.WriteLine("\tn - No");
                            string? choice = Console.ReadLine()?.ToLower();
                            if (choice == null || !Regex.IsMatch(choice, "[yn]"))
                            {
                                Console.WriteLine("Error: Unrecognized input");
                            }
                            else if (choice.Equals("y"))
                            {
                                history.ClearHistory();
                                Console.WriteLine("History deleted");
                            }
                            else
                            {
                                Console.WriteLine("History has not been deleted");
                            }
                        }

                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (Regex.IsMatch(op, "[h]"))
                        {
                            System.Console.WriteLine("Skipping operation calling due to operation being called is history listing");
                            skipSessionCount = true;
                        }
                        else if (double.IsNaN(result))
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
                                case "r":
                                    operation = "Square root";
                                    break;
                                case "p":
                                    operation = "Taking power";
                                    break;
                                case "x":
                                    operation = "Power of 10";
                                    break;
                                default:
                                    Console.WriteLine("Error in Program.cs trying to create entry (line 106)");
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
                        Console.WriteLine($"This is the stacktrace: {e.StackTrace}");
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
            calculator.FinishLogging();// the user was already prompted in the mainbody to choose between a number and digit
            return;
        }
        public static double OperationNumber(CalculationHistory history, bool first)
        {
            if (first)
            {
                Console.WriteLine("Type a number or H/h to choose an number from the history");
            }
            else
            {
                Console.WriteLine("Type another number or H/h to choose an number from the history");
            }
            // now proceed to process the users input


            while (true)
            {

                double result = 0;
                string? numberOrChoice = Console.ReadLine()?.ToLower();
                if (
                    !string.IsNullOrEmpty(numberOrChoice) &&
                    (Regex.IsMatch(numberOrChoice, "[h]") ||
                    Double.TryParse(numberOrChoice, out result))
                    )
                {


                    if (numberOrChoice.Equals("h"))
                    {
                        var count = 0;
                        Console.WriteLine("\n----------- Calculation History ----------");
                        Console.WriteLine($"{"ID",3} {"Operation",-15} {"First Num",-15} {"Second Num",-15} {"Result",-15} {"Date",-15}");
                        foreach (var entry in history.GetAllEntries())
                        {
                            Console.WriteLine($"{count,3} {entry.Operation,-15} {entry.FirstNum,-15:F4} {entry.SecondNum,-15:F4} {entry.Result,-15:F4} {entry.Date,-15}");
                            count++;
                        }

                        while (true)
                        {
                            Console.WriteLine("Choose the result by its id or [E/e]xit back to type a number:");
                            string? choice = Console.ReadLine();

                            if (!string.IsNullOrEmpty(choice) && Regex.IsMatch(choice, "[e]"))
                            {
                                Console.WriteLine("Going back to previous menu");
                                if (first)
                                {
                                    Console.WriteLine("Type a number or H/h to choose an number from the history");
                                }
                                else
                                {
                                    Console.WriteLine("Type another number or H/h to choose an number from the history");
                                }
                                break;
                            }
                            else if (int.TryParse(choice, out int id))
                            {
                                if (id >= 0 && id < history.GetAllEntries().Count)
                                {
                                    return history.GetEntryByID(id).Result;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter an id that's within the history list or exit the submenu");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter an id that's within the history list or exit the submenu");

                            }

                        }
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect value. Please enter a number or choose [H/h]istory to assign a value from past results");
                }
            }
        }
    }
}

