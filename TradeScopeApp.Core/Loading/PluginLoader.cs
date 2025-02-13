using System.Reflection;

namespace TradeScopeApp.Core.Loading
{
    public static class PluginLoader
    {
        public static IEnumerable<IFileLoader> LoadPlugins(string pluginsPath)
        {
            var loaders = new List<IFileLoader>();

            if (!Directory.Exists(pluginsPath))
                return loaders;

            var dllFiles = Directory.GetFiles(pluginsPath, "*.dll", SearchOption.AllDirectories);

            foreach (var dll in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    var types = assembly.GetTypes().Where(t => typeof(IFileLoader).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in types)
                    {
                        if (Activator.CreateInstance(type) is IFileLoader loader)
                        {
                            loaders.Add(loader);
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    foreach (var loaderException in ex.LoaderExceptions)
                    {
                    }
                }
                catch (Exception)
                {
                }
            }

            return loaders;
        }
    }
}
