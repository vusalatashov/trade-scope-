using System.Collections.Generic;
using System.Linq;
using TradeScopeApp.Core.Models;
using TradeScopeApp.Core.Services;
using Xunit;
using FluentAssertions;

namespace TradeScopeApp.Tests.Services
{
    public class TradeDataServiceTests
    {
        [Fact]
        public void AddTradeData_ShouldRaiseTradeDataAddedEvent()
        {
            var service = new TradeDataService();
            IEnumerable<TradeData> testData = new List<TradeData>
            {
                new TradeData
                {
                    Date = new System.DateTime(2023, 10, 1),
                    Open = 100.5m,
                    High = 105.0m,
                    Low = 99.0m,
                    Close = 102.0m,
                    Volume = 5000
                }
            };
            bool eventRaised = false;

            service.TradeDataAdded += (sender, data) =>
            {
                eventRaised = true;
                data.Should().BeEquivalentTo(testData);
            };

            service.AddTradeData(testData);

            eventRaised.Should().BeTrue();
        }
    }
}
