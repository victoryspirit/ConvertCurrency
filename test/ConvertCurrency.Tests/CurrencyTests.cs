using System;
using Xunit;

namespace ConvertCurrency.Tests
{
    public class CurrencyTests
    {
        [Fact]
        public void Test1()
        {
            // arrange 
            var inputParams = "321 456 987,12";
            var currencyObject = new Currency(inputParams);
            var expected = "three hundred twenty-one million four hundred fifty-six thousand nine hundred eighty-seven dollars and twelve cents";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test2()
        {
            // arrange 
            var inputParams = "1";
            var currencyObject = new Currency(inputParams);
            var expected = "one dollar";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void Test3()
        {
            // arrange 
            var inputParams = "0";
            var currencyObject = new Currency(inputParams);
            var expected = "zero dollars";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test4()
        {
            // arrange 
            var inputParams = "25,1";
            var currencyObject = new Currency(inputParams);
            var expected = "twenty-five dollars and ten cents";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test5()
        {
            // arrange 
            var inputParams = "0,01";
            var currencyObject = new Currency(inputParams);
            var expected = "zero dollars and one cent";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test6()
        {
            // arrange 
            var inputParams = "45 100";
            var currencyObject = new Currency(inputParams);
            var expected = "forthy-five thousand one hundred dollars";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test7()
        {
            // arrange 
            var inputParams = "45 023";
            var currencyObject = new Currency(inputParams);
            var expected = "forthy-five thousand twenty-three dollars";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test8()
        {
            // arrange 
            var inputParams = "999 999 999,99";
            var currencyObject = new Currency(inputParams);
            var expected = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents";

            // act
            var actual = currencyObject.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Test9()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new Currency("1 000 000 000,1"));
            Assert.Equal("The maximum number of dollars is 999 999 999.", ex.Message);
        }

        [Fact]
        public void Test10()
        {
            Exception ex = Assert.Throws<System.ArgumentException>(() => new Currency("34,121"));
            Assert.Equal("The maximum number of cents is 99.", ex.Message);
        }
    }
}
