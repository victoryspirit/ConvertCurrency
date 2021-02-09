using System;
using Xunit;

namespace ConvertCurrency.Tests
{
    public class CurrencyTests
    {
        [Fact]
        public void TestValidBigAmount()
        {
            // arrange 
            var inputParams = "321 456 987,12";
            var expected = "three hundred twenty-one million four hundred fifty-six thousand nine hundred eighty-seven dollars and twelve cents";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestOneDollarAmount()
        {
            // arrange 
            var inputParams = "1";
            var expected = "one dollar";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestZeroDollarsAmount()
        {
            // arrange 
            var inputParams = "0";
            var expected = "zero dollars";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAmountWithOneDigitAfterComa()
        {
            // arrange 
            var inputParams = "25,1";
            var expected = "twenty-five dollars and ten cents";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAmountOfOneCent()
        {
            // arrange 
            var inputParams = "0,01";
            var expected = "zero dollars and one cent";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAmountOfThousands()
        {
            // arrange 
            var inputParams = "45 100";
            var expected = "forthy-five thousand one hundred dollars";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAmountWithoutCents()
        {
            // arrange 
            var inputParams = "45 023";
            var expected = "forthy-five thousand twenty-three dollars";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestBiggestValidAmount()
        {
            // arrange 
            var inputParams = "999 999 999,99";
            var expected = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents";

            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInvalidBigAmount()
        {
            // arrange 
            var inputParams = "1 000 000 000,1";
            var expected = "The maximum number of dollars is 999 999 999.";
            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestInvalidCentAmount()
        {
            // arrange 
            var inputParams = "34,121";
            var expected = "The maximum number of cents is 99.";
            // act
            var actual = inputParams.ResultCurrency();

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
