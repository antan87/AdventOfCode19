using AdventOfCode19App.Common;
using Xunit;

namespace XUnitTest.Common
{
    public class IntegerHelperTests
    {
        [Fact]
        public void IntegerHelper_IntArrayToInt()
        {
            int[] array = new[] { 2, 6, 4, 7, 9, 3 };
            int result = IntegerHelper.IntArrayToInt(array);

            Assert.Equal(264793, result);
        }

        [Fact]
        public void IntegerHelper_IntToIntArray()
        {
            int digit = 264793;
            int[] result = IntegerHelper.IntToIntArray(digit);
            int[] expected = new[] { 2, 6, 4, 7, 9, 3 };

            Assert.Equal(expected, result);
        }
    }
}