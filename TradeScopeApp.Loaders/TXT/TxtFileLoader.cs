using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using TradeScopeApp.Core.Loading;
using TradeScopeApp.Core.Models;

namespace TradeScopeApp.Loaders.TXT
{
    public class TxtFileLoader : IFileLoader
    {
        public bool CanLoad(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".txt", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<TradeData> Load(string filePath)
        {
            Log.Information("Loading file: {FilePath}", filePath);
            var tradeDataList = new List<TradeData>();

            try
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');

                    if (parts.Length != 6)
                        continue;

                    var trade = new TradeData
                    {
                        Date = DateTime.Parse(parts[0]),
                        Open = decimal.Parse(parts[1]),
                        High = decimal.Parse(parts[2]),
                        Low = decimal.Parse(parts[3]),
                        Close = decimal.Parse(parts[4]),
                        Volume = long.Parse(parts[5])
                    };

                    tradeDataList.Add(trade);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return tradeDataList;
        }
    }
}
