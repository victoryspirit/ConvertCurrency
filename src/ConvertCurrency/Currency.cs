using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConvertCurrency
{
    public class Currency
    {
        public static string[] SingleDigitsWord = new string[] { null, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        public static string[] DoubleDigitsWord = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        public static string[] SimpleDoubleDigitsWord = new string[] { "twenty", "thirty", "forthy", "fifty", "sixty", "seventy", "eighty", "ninety" };
        public Currency(string inputNumber)
        {
            this.inputNumber = inputNumber;
            
            string[] dollarsAndCents = inputNumber.Split( new string[] {","}, StringSplitOptions.None);
            string dollars = dollarsAndCents[0];
            if (inputNumber.IndexOf(",") > 0 && dollarsAndCents[1].Length > 2)
            {
                throw new System.ArgumentException("The maximum number of cents is 99.");
            }

            if (Int32.Parse(Regex.Replace(dollars, @"\s+", String.Empty)) > 999999999)
            {
                throw new System.ArgumentException("The maximum number of dollars is 999 999 999.");
            }
        }
        
        public struct Entry
        {
            public int Number;
            public string RankName;
            public Entry(int number, string rankName)
            {
                Number = number;
                RankName = rankName;
            }
        }

        public static int[] DollarDigits(int number)
        {
            var dollarArray = new int[3];
            dollarArray[0] = number/100;
            dollarArray[1] = (number%100)/10;
            dollarArray[2] = (number%100)%10;
            return dollarArray;
        }

        public static int[] CentDigits(int number)
        {
            var centArray = new int[2];
            centArray[0] = number/10;
            centArray[1] = number%10;
            return centArray;
        }

        public static List<Entry> SplitNumbers(int number) 
        {
            var masive = new List<Entry>();
            masive.Add(new Entry(number/1000000, "million"));
            masive.Add(new Entry((number%1000000)/1000, "thousand"));
            masive.Add(new Entry((number%1000000)%1000, ""));
            return masive;
        }

        public static string SimpleDoubleDigits(int doubles, int ones)
        {
            string word1 = SimpleDoubleDigitsWord[doubles - 2];
            string word2 = SingleDigitsWord[ones];
            if (String.IsNullOrEmpty(word2))
            {
                return word1;
            }
            else
            {
                string fullWord = String.Concat(word1, "-", word2);
                return fullWord;
            }
        }

        public static string DoubleDigitsFor(int doubles, int singles)
        {
            switch (doubles)
            {
                case 0:
                    return SingleDigitsWord[singles];
                case 1:
                    return DoubleDigitsWord[singles];
                default:
                    return SimpleDoubleDigits(doubles, singles);
            }
        }
        
        public static string HundredDigits(int number)
        {
            string result = SingleDigitsWord[number] + " hundred";
            return result;
        }

        public static void WriteToConsole(List<Entry> list)
        {
            foreach (Entry entry in list)
            {
                Console.WriteLine(entry.Number);
                Console.WriteLine(entry.RankName);
            }
        }

        public static List<string> WordForRank(string rankText, int hundreds, int tens, int ones)
        {
            List<string> words = new List<string>();

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

            if (!String.IsNullOrEmpty(rankText))
            {
                words.Add(rankText);
            }

            List<string> noneEmptyWords = words.Where(x => !string.IsNullOrEmpty(x)).ToList<string>();
            return noneEmptyWords;
        }

        public static string DollarText(string dollar)
        {
            List<string> wordList = new List<string>();

            if (dollar.Equals("1"))
            {
                return SingleDigitsWord[1] + " dollar";
            }

            int dollarInt = Int32.Parse(dollar);
            List<Entry> arrayOfRanks = SplitNumbers(dollarInt);
            foreach (Entry element in arrayOfRanks)
            {
                if ((element.Number == 0) && (!String.IsNullOrEmpty(element.RankName)))
                {
                    continue;
                }
                int[] dollarParams = DollarDigits(element.Number);
                List<string> listOfWords = WordForRank(element.RankName, dollarParams[0], dollarParams[1], dollarParams[2]);
                listOfWords.ForEach(listElement => wordList.Add(listElement));
            }

            wordList.Add("dollars");
            return String.Join(" ", wordList);
        }

        public static string CentText(string cent)
        {
            List<string> wordList = new List<string>();
            int centInt = Int16.Parse(cent);
            
            if (centInt == 0)
            {
                return null;
            }
            int[] centParams = CentDigits(centInt);
            string digitText = DoubleDigitsFor(centParams[0], centParams[1]);
            wordList.Add(digitText);

            string centRank = (centInt == 1) ? "cent" : "cents";
            wordList.Add(centRank);

            return String.Join(" ", wordList);
        }

        public string ResultCurrency()
        {
            string withoutSpaces = Regex.Replace(this.inputNumber, @"\s+", String.Empty);
            string numberReplaced = withoutSpaces.Replace(',', '.');
            decimal number = Decimal.Parse(numberReplaced);
            

            string numberTwoDigits = number.ToString("0.00");
            
            string[] dollarsAndCents = numberTwoDigits.Split( new string[] {"."}, StringSplitOptions.None);
            string dollars = dollarsAndCents[0];
            string cents = dollarsAndCents[1];

            

            string  dollarContext = DollarText(dollars);
            string  centContext = CentText(cents);

            if (!String.IsNullOrEmpty(centContext))
            {
                string[] finalArray = new string[2] { dollarContext, centContext };
                return String.Join(" and ", finalArray);
            }
            else
            {
                return dollarContext;
            }
        }
        private string inputNumber;
    }
}