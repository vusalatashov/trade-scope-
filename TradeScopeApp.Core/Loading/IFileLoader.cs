using TradeScopeApp.Core.Models;

namespace TTradeScopeApp.Core.Loading
{
    public interface IFileLoader
    {
        bool CanLoad(string filePath);
        IEnumerable<TradeData> Load(string filePath);
    }
}
