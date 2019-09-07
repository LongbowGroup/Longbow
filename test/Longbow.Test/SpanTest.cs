#if NETCore
using System;
using System.Linq;
using Xunit;

namespace Longbow
{
    
    public class SpanTest
    {
        [Theory]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void String_Source_EmptyOrNull(string source, string splitStr)
        {
            Assert.True(source.SpanSplit(splitStr).Count == 0);
            Assert.True(source.SpanSplit(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 0);

            Assert.True(source.SpanSplitAny(splitStr).Count == 0);
            Assert.True(source.SpanSplitAny(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 0);
        }

        [Theory]
        [InlineData("12\r\n", "")]
        [InlineData("12\r\n", null)]
        public void String_Split_EmptyOrNull(string source, string splitStr)
        {
            Assert.True(source.SpanSplit(splitStr).Count == 1);
            Assert.True(source.SpanSplit(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 1);
            Assert.True(source.SpanSplitAny(splitStr).Count == 1);
            Assert.True(source.SpanSplitAny(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 1);
        }

        [Theory]
        [InlineData("12;34;56", ";")]
        [InlineData("12 ; 34; 56 ", ";")]
        [InlineData("12;, 34;,56", ";,")]
        public void String_SpanSplit_Ok(string source, string splitStr)
        {
            Assert.True(source.SpanSplit(splitStr).Count == 3);
            Assert.True(source.SpanSplit(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 3);

            Assert.True(source.SpanSplitAny(splitStr).Count == 3);
            Assert.True(source.SpanSplitAny(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 3);
        }

        [Theory]
        [InlineData("12;34;56", "~")]
        public void String_SpanSplit_NotFound(string source, string splitStr)
        {
            Assert.True(source.SpanSplit(splitStr).Count == 1);
            Assert.True(source.SpanSplit(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 1);

            Assert.True(source.SpanSplitAny(splitStr).Count == 1);
            Assert.True(source.SpanSplitAny(splitStr, StringSplitOptions.RemoveEmptyEntries).Count == 1);
        }

        [Theory]
        [InlineData(null, new byte[0])]
        [InlineData(new byte[0], new byte[0])]
        public void Byte_Source_Null(byte[] source, byte[] splitStr)
        {
            source.SpanSplit(splitStr);
            source.SpanSplitAny(splitStr);
        }

        [Theory]
        [InlineData(new byte[] { 0x31, 0x32, 0x33 }, null)]
        [InlineData(new byte[] { 0x31, 0x32, 0x33 }, new byte[0])]
        public void Byte_Splite_Null(byte[] source, byte[] splitStr)
        {
            Assert.True(source.SpanSplit(splitStr).Count == 1);
            Assert.True(source.SpanSplitAny(splitStr).Count == 1);
        }

        [Theory]
        [InlineData(new byte[] { 0x31, 0x32, 0x33 }, new byte[] { 0x32 })]
        [InlineData(new byte[] { 0x31, 0x32, 0x33, 0x34 }, new byte[] { 0x32, 0x33 })]
        public void Byte_SpanSplite_Ok(byte[] source, byte[] splitStr)
        {
            Assert.True(source.SpanSplit(splitStr).Count == 2);
            Assert.True(source.SpanSplitAny(splitStr).Count == 2);
        }

        [Theory]
        [InlineData(new byte[] { 0x31, 0x32, 0x33 }, new byte[] { 0x34 })]
        [InlineData(new byte[] { 0x31, 0x32, 0x33, 0x34 }, new byte[] { 0x35, 0x36 })]
        public void Byte_SpanSplite_NotFound(byte[] source, byte[] splitStr)
        {
            Assert.True(source.SpanSplit(splitStr)[0].AsSpan().SequenceEqual(source.AsSpan()));
            Assert.True(source.SpanSplitAny(splitStr)[0].AsSpan().SequenceEqual(source.AsSpan()));
        }
    }
}
#endif
