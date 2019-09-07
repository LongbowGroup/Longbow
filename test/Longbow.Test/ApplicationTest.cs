using Xunit;

namespace Longbow
{
    
    public class ApplicationTest
    {
        [Fact]
        public void HasInstance_Ok()
        {
            LgbApplication.HasInstance();
            Assert.True(LgbApplication.HasInstance("dotnet"));
        }
    }
}
