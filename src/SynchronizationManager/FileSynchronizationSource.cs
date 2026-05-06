using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SynchronizationManager;

public interface IConfigurationSource
{
    IEnumerable<Configuration> GetConfigurations();
}

public class FileConfigurationSource(string pathToConfigurationFiles, ILogger logger) : IConfigurationSource
{
    private const string ExtensionNameForConfigFiles = "fsc";

    public IEnumerable<Configuration> GetConfigurations()
    {
        var allConfigFiles = Directory.GetFiles(pathToConfigurationFiles, $"*.{ExtensionNameForConfigFiles}", SearchOption.TopDirectoryOnly);

        foreach (var configFileName in allConfigFiles)
        {
            Configuration? configuration = null;

            try
            {
                var jsonConfig = File.ReadAllText(configFileName);
                configuration = JsonConvert.DeserializeObject<Configuration>(jsonConfig);
            }
            catch (Exception e)
            {
                logger.LogError("Can't read config in file {0}, skipp. Inner exception {1}", configFileName, e);
            }

            if (configuration is not null)
                yield return configuration;
        }
    }
}