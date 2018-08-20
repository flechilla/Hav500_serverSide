using System;
using Xunit;

namespace Havana500.Business.Tests
{
    ///TODO: Study about how to use the different methods inside the Assert class. These methods are
    ///used for different purposes
    /// TODO: Have to implement a common library that will contain functionalities that will be used for all the tests projects.
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = 4;
            Assert.Equal(4, x);
        }
    }
}
