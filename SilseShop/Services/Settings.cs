using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SilseShop.Services
{
    class Settings
    {
        public static string ConnectionString { get; private set; }
        public static Dictionary<string, string> Configuration { get; set; }

        static Settings()
        {
            Configuration = new Dictionary<string, string>();
#if DEBUG
            if (!Directory.Exists(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Configs"))
            {
                throw new Exception("Отсутствует дирректория - \"" + Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Configs\"");
            }

            string[] pathsConfig = Directory.GetFiles(Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Configs", "*.json", SearchOption.TopDirectoryOnly);
            if (pathsConfig.Length == 0)
            {
                throw new Exception("Отсутствует файл конфигурации в дирректории - \"" + Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Configs\"");
            }

            foreach (string path in pathsConfig)
            {
                string json = "";
                using (StreamReader sr = new StreamReader(path))
                    json = sr.ReadToEnd();
                var cfg = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                foreach (var item in cfg)
                    Configuration[item.Key] = item.Value;
            }

            // Загружает config.json в debug режиме, заменяя значения при совпадении ключей.
            string jsonLast = "";
            using (StreamReader sr = new StreamReader($"Configs{Path.DirectorySeparatorChar}config.json"))
                jsonLast = sr.ReadToEnd();
            var cfgLast = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonLast);
            foreach (var item in cfgLast)
                Configuration[item.Key] = item.Value;
#else
            string[] k = Environment.GetEnvironmentVariables().Keys.Cast<string>().ToArray();
            string[] v = Environment.GetEnvironmentVariables().Values.Cast<string>().ToArray(); ;
            Configuration = new Dictionary<string, string>();
            for (int i = 0; i < k.Length; i++)
                Configuration[k[i]] = v[i] ?? "";

#endif
            ConnectionString = Configuration["connection_string"];
        }
    }
}
