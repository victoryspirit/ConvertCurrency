using System;

namespace ConvertCurrency
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Enter currency: "); 
            var inputParameter = Console.ReadLine();
            while (inputParameter.ToLower() != "exit")
            {
                try
                {
                    var currency = inputParameter.ResultCurrency();
                    Console.WriteLine(currency);
                    Console.WriteLine("Type 'exit' to close the program.");
                    Console.WriteLine("Enter currency: ");
                    inputParameter = Console.ReadLine();
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Given currency is invalid! The maximum number of dollars is 999 999 999 and cents is 99.");
                }
            }
        }
    }
}
