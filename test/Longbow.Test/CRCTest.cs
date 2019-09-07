using System;
using Xunit;

namespace Longbow
{
    
    public class CRCTest
    {
        [Theory]
        [InlineData(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x35, 0x36 }, 50084)]
        [InlineData(new byte[0], 0)]
        public void CRC16_Ok(byte[] data, int result)
        {
            Assert.Equal(result, LgbCRC16.ComputerHash(data));
        }

        [Theory]
        [InlineData(new byte[] { 0x31, 0x32, 0x33, 0x34, 0x35, 0x35, 0x36 }, 204016330)]
        [InlineData(new byte[0], 0)]
        public void CRC32_Ok(byte[] data, int result)
        {
            Assert.Equal(result, LgbCRC32.ComputerHash(data));
        }

        [Fact]
        public void CRC_Error()
        {
            Assert.Throws<ArgumentNullException>(() => LgbCRC16.ComputerHash(null));
            Assert.Throws<ArgumentNullException>(() => LgbCRC32.ComputerHash(null));
        }

        [Fact]
        public void ComputerHash_Ok()
        {
            Assert.Equal(850, LgbCRC16.ComputerHash(new byte[] { 0, 1, 3 }, CRC16Option.XModen));
            Assert.Equal(21186, LgbCRC16.ComputerHash(new byte[] { 2, 4, 6 }, CRC16Option.Modbus));
            Assert.Throws<ArgumentNullException>(() => LgbCRC16.ComputerHash(null, CRC16Option.XModen));
        }
    }
}
