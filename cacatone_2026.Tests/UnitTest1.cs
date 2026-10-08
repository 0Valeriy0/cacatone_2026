using Xunit;
using Domain.Models;

namespace cacatone_2026.Tests
{
    public class UnitTest
    {
        [Fact]
        public void TestClassProperties()
        {
            // Arrange
            var test = new Test()
            {   
                Id = 0,
                Name = "Sample Test",
            };

            // Act & Assert
            Assert.NotNull(test);
        }
    }
}