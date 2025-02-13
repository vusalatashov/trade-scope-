using System.Collections.Generic;
using System.IO;
using System.Linq;
using TradeScopeApp.Core.Models;
using TradeScopeApp.Loaders.CSV;
using Xunit;
using FluentAssertions;

namespace TradeScopeApp.Tests.Loaders
{
    public class CsvFileLoaderTests
    {
        [Fact]
        public void CanLoad_ShouldReturnTrue_ForCsvFiles()
        {
            var loader = new CsvFileLoader();
            var filePath = "sample.csv";

            var result = loader.CanLoad(filePath);

            result.Should().BeTrue();
        }

        [Fact]
        public void Load_ShouldParseValidCsvFile()
        {
            var loader = new CsvFileLoader();
            var csvContent = @"Date,Open,High,Low,Close,Volume
2023-10-01,100.5,105.0,99.0,102.0,5000
2023-10-02,102.0,106.0,101.0,105.0,6000";
            var filePath = "test.csv";
            File.WriteAllText(filePath, csvContent);

            IEnumerable<TradeData> result = loader.Load(filePath);

            File.Delete(filePath);

            result.Should().NotBeNull();
            result.Count().Should().Be(2);

            var firstRecord = result.First();
            firstRecord.Date.Should().Be(new System.DateTime(2023, 10, 1));
            firstRecord.Open.Should().Be(100.5m);
            firstRecord.High.Should().Be(105.0m);
            firstRecord.Low.Should().Be(99.0m);
            firstRecord.Close.Should().Be(102.0m);
            firstRecord.Volume.Should().Be(5000);
        }
    }
}
