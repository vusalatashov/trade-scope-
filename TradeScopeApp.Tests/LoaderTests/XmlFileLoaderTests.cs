using System.Collections.Generic;
using System.IO;
using System.Linq;
using TradeScopeApp.Core.Models;
using TradeScopeApp.Loaders.XML;
using Xunit;
using FluentAssertions;

namespace TradeScopeApp.Tests.Loaders
{
    public class XmlFileLoaderTests
    {
        [Fact]
        public void CanLoad_ShouldReturnTrue_ForXmlFiles()
        {
            var loader = new XmlFileLoader();
            var filePath = "sample.xml";

            var result = loader.CanLoad(filePath);

            result.Should().BeTrue();
        }

        [Fact]
        public void Load_ShouldParseValidXmlFile()
        {
            var loader = new XmlFileLoader();
            var xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Trades>
    <Trade>
        <Date>2023-10-01</Date>
        <Open>100.5</Open>
        <High>105.0</High>
        <Low>99.0</Low>
        <Close>102.0</Close>
        <Volume>5000</Volume>
    </Trade>
    <Trade>
        <Date>2023-10-02</Date>
        <Open>102.0</Open>
        <High>106.0</High>
        <Low>101.0</Low>
        <Close>105.0</Close>
        <Volume>6000</Volume>
    </Trade>
</Trades>";
            var filePath = "test.xml";
            File.WriteAllText(filePath, xmlContent);

            IEnumerable<TradeData> result = loader.Load(filePath);

            File.Delete(filePath);

            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }
    }
}
