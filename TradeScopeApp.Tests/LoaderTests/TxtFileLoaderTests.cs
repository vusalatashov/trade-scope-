using System.Collections.Generic;
using System.IO;
using System.Linq;
using TradeScopeApp.Core.Models;
using TradeScopeApp.Loaders.TXT;
using Xunit;
using FluentAssertions;

namespace TradeScopeApp.Tests.Loaders
{
    public class TxtFileLoaderTests
    {
        [Fact]
        public void CanLoad_ShouldReturnTrue_ForTxtFiles()
        {
            var loader = new TxtFileLoader();
            var filePath = "sample.txt";

            var result = loader.CanLoad(filePath);

            result.Should().BeTrue();
        }

        [Fact]
        public void Load_ShouldParseValidTxtFile()
        {
            var loader = new TxtFileLoader();
            var txtContent = @"2023-10-01,100.5,105.0,99.0,102.0,5000
2023-10-02,102.0,106.0,101.0,105.0,6000";
            var filePath = "test.txt";
            File.WriteAllText(filePath, txtContent);

            IEnumerable<TradeData> result = loader.Load(filePath);

            File.Delete(filePath);

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }
    }
}
