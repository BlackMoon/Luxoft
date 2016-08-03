using MoneyRest;
using Xunit;

namespace MoneyRestTest
{
    public class MoneySummTests
    {
        [Fact]
        public void CalculateSummandsTest1()
        {
            decimal [] numbers = { 100, 100 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();
            
            Assert.Equal(ms.Summands, new decimal[]{100});    
        }

        [Fact]
        public void CalculateSummandsTest2()
        {
            decimal[] numbers = { 100, 100 };
            MoneySumm ms = new MoneySumm(numbers);

            ms.CalculateSummands();

            Assert.Equal(ms.Summands, new decimal[] { 100 });
        }
    }
}
