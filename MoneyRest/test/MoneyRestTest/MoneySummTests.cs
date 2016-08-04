using System.Collections.Generic;
using MoneyRest;
using Xunit;

namespace MoneyRestTest
{
    public class MoneySummTests
    {
        [Fact]
        public void CalculateSummandsTest1()
        {
            long [] numbers = { 100, 100 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();
            
            Assert.Equal(ms.Summands, new List<long>(){100});    
        }

        [Fact]
        public void CalculateSummandsTest2()
        {
            long[] numbers = { 100, 50, 70, 120, 150 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();

            Assert.Equal(ms.Summands, null);
        }

        [Fact]
        public void CalculateSummandsTest3()
        {
            long[] numbers = { 12, 1, 9, 7, 3, 5 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();

            Assert.Equal(ms.Summands, new List<long>() { 3, 9});
        }

        [Fact]
        public void CalculateSummandsTest4()
        {
            long[] numbers = { 30, 1, 2 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();

            Assert.Equal(ms.Summands, null);
        }

        [Fact]
        public void CalculateSummandsTest5()
        {
            long[] numbers = { 30, 60, 15, 9, 9, 9, 8, 8, 5, 5, 3, 3, 3, 3, 3, 2, 2, 2, 1 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();

            Assert.Equal(ms.Summands, new List<long>() { 5, 8, 8, 9} );
        }

        [Fact]
        public void CalculateSummandsTest6()
        {
            long[] numbers = { 50, 49, 47, 30, 15, 10, 10 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();

            Assert.Equal(ms.Summands, new List<long>() { 10, 10, 30 });
        }
    }
}
