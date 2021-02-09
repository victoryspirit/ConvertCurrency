using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConvertCurrency
{
    public static class CurrencyExtensions
    {
       public static string ResultCurrency(this string inputNumber)
       {
           var withoutSpaces = Regex.Replace(inputNumber, @"\s+", string.Empty);
           var numberReplaced = withoutSpaces.Replace(',', '.');

           if (decimal.TryParse(numberReplaced, out var number))
           {
               if (!ValidCurrency(numberReplaced))
               {
                   return "The maximum number of cents is 99.";
               }
               var numberTwoDigits = number.ToString("0.00");
               var dollarsAndCents = numberTwoDigits.Split( new[] {"."}, StringSplitOptions.None);
               var dollars = dollarsAndCents[0];
               var cents = dollarsAndCents[1];

               if (IsIntTooBig(int.Parse(Regex.Replace(dollars, @"\s+", string.Empty))))
               {
                   return "The maximum number of dollars is 999 999 999.";
               }

               var  dollarContext = DollarText(dollars);
               var  centContext = CentText(cents);

               if (!string.IsNullOrEmpty(centContext))
               {
                   var finalArray = new[] { dollarContext, centContext };
                   return string.Join(" and ", finalArray);
               }
               else
               {
                   return dollarContext;
               }
           }
           else
           { 
               return "Should be a number!";
               
           }
       }

       private static bool ValidCurrency(string number)
       {
           return (number.IndexOf(".", StringComparison.Ordinal) < 0) || 
                  (number.IndexOf(".", StringComparison.Ordinal) > 0 && number.Split(new[] {"."}, StringSplitOptions.None)[1].Length <= 2);
       }

       private static bool IsIntTooBig(int number)
       {
           return number > 999_999_999;
       }

        private static int[] DollarDigits(int number)
       {
           var dollarArray = new int[3];
           dollarArray[0] = number / 100;
           dollarArray[1] = (number % 100) / 10;
           dollarArray[2] = (number % 100) % 10;
           return dollarArray;
       }

       private static int[] CentDigits(int number)
       {
           var centArray = new int[2];
           centArray[0] = number / 10;
           centArray[1] = number % 10;
           return centArray;
       }

       private static List<Entry> SplitNumbers(int number) 
       {
           var masive = new List<Entry>();
           masive.Add(new Entry { Number = number / 1_000_000, RankName= "million" });
           masive.Add(new Entry { Number = (number % 1_000_000) / 1_000, RankName = "thousand"});
           masive.Add(new Entry { Number = (number % 1_000_000) % 1_000, RankName = ""});
           return masive;
       }

       private static string SimpleDoubleDigits(int doubles, int ones)
       {
           var word1 = Words.SimpleDoubleDigitsWord[doubles - 2];
           var word2 = Words.SingleDigitsWord[ones];
           if (string.IsNullOrEmpty(word2))
           {
               return word1;
           }
           else
           {
               var fullWord = string.Concat(word1, "-", word2);
               return fullWord;
           }
       }

       private static string DoubleDigitsFor(int doubles, int singles)
       {
           switch (doubles)
           {
               case 0:
                   return Words.SingleDigitsWord[singles];
               case 1:
                   return Words.DoubleDigitsWord[singles];
               default:
                   return SimpleDoubleDigits(doubles, singles);
           }
       }
        
       private static string HundredDigits(int number)
       {
           var result = Words.SingleDigitsWord[number] + " hundred";
           return result;
       }

       private static List<string> WordForRank(string rankText, int hundreds, int tens, int ones)
       {
           var words = new List<string>();

           if (hundreds != 0)
           {
               words.Add(HundredDigits(hundreds));
           }

           if ((hundreds == 0) && (tens == 0) && (ones == 0))
           {
               words.Add("zero");
           }
           else
           {
               words.Add(DoubleDigitsFor(tens, ones));
           }

           if (!string.IsNullOrEmpty(rankText))
           {
               words.Add(rankText);
           }

           var noneEmptyWords = words.Where(x => !string.IsNullOrEmpty(x)).ToList<string>();
           return noneEmptyWords;
       }

       private static string DollarText(string dollar)
       {
           var wordList = new List<string>();

           if (dollar.Equals("1"))
           {
               return Words.SingleDigitsWord[1] + " dollar";
           }

           var dollarInt = int.Parse(dollar);
           var arrayOfRanks = SplitNumbers(dollarInt);
           foreach (var element in arrayOfRanks)
           {
               if ((element.Number == 0) && (!string.IsNullOrEmpty(element.RankName)))
               {
                   continue;
               }
               var dollarParams = DollarDigits(element.Number);
               var listOfWords = WordForRank(element.RankName, dollarParams[0], dollarParams[1], dollarParams[2]);
               listOfWords.ForEach(listElement => wordList.Add(listElement));
           }

           wordList.Add("dollars");
           return string.Join(" ", wordList);
       }

       private static string CentText(string cent)
       {
           var wordList = new List<string>();
           int centInt = short.Parse(cent);
            
           if (centInt == 0)
           {
               return null;
           }
           var centParams = CentDigits(centInt);
           var digitText = DoubleDigitsFor(centParams[0], centParams[1]);
           wordList.Add(digitText);

           var centRank = (centInt == 1) ? "cent" : "cents";
           wordList.Add(centRank);

           return string.Join(" ", wordList);
       }


    }
}