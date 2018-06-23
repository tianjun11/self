using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest
{
    public class Test
    {
        [Fact]
        public void Test_Fibonacci_N()
        {
            var act = Fibonacci(10);
            var expect = 55;
            Assert.True(act == expect);
        }
    }
}
