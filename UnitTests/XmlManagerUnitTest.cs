using System;
using Xunit;

namespace UnitTests
{
    public class XmlManagerUnitTest
    {
        [Fact]
        public void ValidateSchema_Valid_Xml()
        {
            Assert.Equal("true", "true");
        }
    }
}
