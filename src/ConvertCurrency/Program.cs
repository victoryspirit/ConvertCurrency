using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConvertCurrency
{
    class Program
    {
        public class ConsoleRetriever {
            public static string GetInput()
            {
                return Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter currency: "); 
            string inputParameter = ConsoleRetriever.GetInput();
            try
            {
                var currency = new Currency(inputParameter);
                string finalText = currency.ResultCurrency();
                Console.WriteLine(finalText);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("Given currency is invalid! The maximum number of dollars is 999 999 999 and cents is 99.");
            }
            
        }
    }
}
