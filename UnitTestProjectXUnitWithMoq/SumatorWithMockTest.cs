using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace UnitTestProjectXUnitWithMoq
{
    public class Data
    {
        public int Expected { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    public interface IDataService
    {
        Data GetSomeData(int expected);
    }
    
    public class Summator
    {
        private readonly IDataService _dataService;

        public Summator(IDataService dataService)
        {
            _dataService = dataService;
        }
        
        public int GetSummByExpected(int expected)
        {
            var data = _dataService.GetSomeData(expected);
            return data.X + data.Y;
        }
    }

    public class SumatorWithMockTest
    {
        private readonly Summator _summator;
        private readonly Mock<IDataService> _dataServiceMock;

        public SumatorWithMockTest()
        {
            _dataServiceMock = new Mock<IDataService>();
            _summator = new Summator(_dataServiceMock.Object);
        }

        [Fact]
        public void Moq_GetSummTest_2_3_Equals_5()
        {
            _dataServiceMock
                .Setup(expression => expression.GetSomeData(It.IsAny<int>()))
                .Returns(new Data {Expected = 5, X = 2, Y = 3});
            
            Assert.Equal(5, _summator.GetSummByExpected(5));
        }
        
        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        public void Moq_InlineDataGetSummTest(int expected)
        {
            var dataList = new List<Data>
            {
                new Data {Expected = 5, X = 2, Y = 3},
                new Data {Expected = 6, X = 2, Y = 3}
            };
            
            _dataServiceMock
                .Setup(expression => 
                    expression.GetSomeData(It.IsAny<int>()))
                .Returns(
                    dataList.FirstOrDefault(p => p.Expected == expected));

            switch (expected)
            {
                case 5:
                    Assert.Equal(expected, _summator.GetSummByExpected(expected));
                    break;
                case 6:
                    Assert.NotEqual(expected, _summator.GetSummByExpected(expected));
                    break;
                default:
                    return;
            }
        }
    }

}