using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace ProjectRenamer
{
    public class ConfigService
    {
        private Config _config;
        public ConfigService()
        {
            _config = Des();
            FolderNames = _config.FolderNames;
            SourceName = _config.SourceName;
        }

        public string[] FolderNames { get; }
        public string SourceName { get; }

        private Config Des()
        {
            var readFile = File.ReadAllText(@"E:\A-Level\ProjectRenamer\ProjectRenamer\config.json");
            var config = JsonSerializer.Deserialize<Config>(readFile);
            return config;
        }
    }
}
