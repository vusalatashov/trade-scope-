using System;
using System.Collections.Generic;
using TradeScopeApp.Core.Models;

namespace TradeScopeApp.Core.Services
{
    public class TradeDataService
    {
        public event EventHandler<IEnumerable<TradeData>> TradeDataAdded;

        public void AddTradeData(IEnumerable<TradeData> data)
        {
            TradeDataAdded?.Invoke(this, data);
        }
    }
}
