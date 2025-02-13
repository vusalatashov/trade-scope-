using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Serilog;
using TradeScopeApp.Core.Loading;
using TradeScopeApp.Core.Models;

namespace TradeScopeApp.Loaders.XML
{
    public class XmlFileLoader : IFileLoader
    {
        public bool CanLoad(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".xml", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<TradeData> Load(string filePath)
        {
            Log.Information("Loading file: {FilePath}", filePath);
            var tradeDataList = new List<TradeData>();

            try
            {
                var doc = XDocument.Load(filePath);
                tradeDataList = doc.Descendants("Trade")
                    .Select(x => new TradeData
                    {
                        Date = DateTime.Parse(x.Element("Date")?.Value ?? string.Empty),
                        Open = decimal.Parse(x.Element("Open")?.Value ?? "0"),
                        High = decimal.Parse(x.Element("High")?.Value ?? "0"),
                        Low = decimal.Parse(x.Element("Low")?.Value ?? "0"),
                        Close = decimal.Parse(x.Element("Close")?.Value ?? "0"),
                        Volume = long.Parse(x.Element("Volume")?.Value ?? "0")
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            return tradeDataList;
        }
    }
}
