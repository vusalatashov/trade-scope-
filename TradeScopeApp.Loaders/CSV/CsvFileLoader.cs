using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Serilog;
using TradeScopeApp.Core.Loading;
using TradeScopeApp.Core.Models;

namespace TradeScopeApp.Loaders.CSV
{
    public class CsvFileLoader : IFileLoader
    {
        public bool CanLoad(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase);
        }

        public IEnumerable<TradeData> Load(string filePath)
        {
            Log.Information("Loading file: {FilePath}", filePath);
            var tradeDataList = new List<TradeData>();

            try
            {
                using var reader = new StreamReader(filePath);
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    MissingFieldFound = null,
                    HeaderValidated = null
                };

                using var csv = new CsvReader(reader, csvConfig);
                tradeDataList = new List<TradeData>(csv.GetRecords<TradeData>());
            }
            catch (Exception ex)
            {
                throw;
            }

            return tradeDataList;
        }
    }
}
