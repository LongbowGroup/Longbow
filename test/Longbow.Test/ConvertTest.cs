using System;
using System.Net.Sockets;
using Xunit;

namespace Longbow
{
    
    public class ConvertTest
    {
        [Theory]
        [InlineData(null, 0)]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("true", 1)]
        public void Convert_Ok(string value, int v)
        {
            Assert.Equal(v, LgbConvert.ReadValue(value, v));
        }

        [Fact]
        public void Class_Ok()
        {
            var v = new Foo();
            Assert.Equal(v, LgbConvert.ReadValue("Test", v));
        }

        [Theory]
        [InlineData("AccessDenied")]
        [InlineData("10013")]
        [InlineData("Test")]
        public void Enum_Ok(string value)
        {
            Assert.Equal(SocketError.AccessDenied, LgbConvert.ReadValue(value, SocketError.AccessDenied));
        }

        [Theory]
        [InlineData("12", 12)]
        public void Nullable_Ok(string value, int? expect)
        {
            var v = expect;
            Assert.Equal(expect, LgbConvert.ReadValue(value, v));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Default_Ok(object source)
        {
            Assert.Equal(0, LgbConvert.ReadValue(source, 0));
        }

        [Fact]
        public void DBNUll_Ok()
        {
            Assert.Equal(0, LgbConvert.ReadValue(DBNull.Value, 0));
        }

        [Fact]
        public void Object_Ok()
        {
            var v = new object();
            Assert.Equal("test", LgbConvert.ReadValue("test", v).ToString());
        }

        private class Foo
        {

        }
    }
}
