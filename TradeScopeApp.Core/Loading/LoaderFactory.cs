using System.Collections.Generic;
using System.Linq;
using Serilog;

namespace TradeScopeApp.Core.Loading
{
    public class LoaderFactory
    {
        private readonly IEnumerable<IFileLoader> _loaders;

        public LoaderFactory(IEnumerable<IFileLoader> loaders)
        {
            _loaders = loaders;
        }

        public IFileLoader GetLoader(string filePath)
        {
            var loader = _loaders.FirstOrDefault(l => l.CanLoad(filePath));
            if (loader != null)
            {
                Log.Information("Loader {LoaderType} selected for file: {FilePath}", loader.GetType().Name, filePath);
            }
            else
            {
                Log.Warning("No loader found for file: {FilePath}", filePath);
            }
            return loader;
        }

    }
}
