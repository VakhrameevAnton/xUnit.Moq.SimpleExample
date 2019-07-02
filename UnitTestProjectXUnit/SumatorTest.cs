using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProjectXUnit
{
    public class Summator
    {
        public int GetSumm(int x, int y)
        {
            return x + y;
        }
    }
    
    public class SumatorTest
    {
        private Summator _summator;

        public SumatorTest()
        {
            _summator = new Summator();
        }


        [Fact]
        public void GetSummTest_2_3_Equals_5()
        {
            Assert.Equal(5, _summator.GetSumm(2, 3));
        }
        
        [Fact]
        public void GetSummTest_2_3_NotEquals_5()
        {
            Assert.NotEqual(6, _summator.GetSumm(2, 3));
        }

        [Theory]
        [InlineData(2,3,5)]
        [InlineData(2,3,6)]
        public void InlineDataGetSummTest(int x, int y, int expected)
        {
            switch (expected)
            {
                case 5:
                    Assert.Equal(expected, _summator.GetSumm(x, y));
                    break;
                case 6:
                    Assert.NotEqual(expected, _summator.GetSumm(x, y));
                    break;
                default:
                    return;
            }
        }
        
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void ClassDataGetSummTest(int x, int y, int expected)
        {
            switch (expected)
            {
                case 5:
                    Assert.Equal(expected, _summator.GetSumm(x, y));
                    break;
                case 6:
                    Assert.NotEqual(expected, _summator.GetSumm(x, y));
                    break;
                default:
                    return;
            }
        }
        
    }

    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {2, 3, 5},
            new object[] {2, 3, 6}
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
