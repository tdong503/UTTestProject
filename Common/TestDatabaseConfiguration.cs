using System.IO;
using Newtonsoft.Json;

namespace Common
{
    public class TestDatabaseConfiguration
    {
        public static string GetConnectionString()
        {
            var jsonString = File.ReadAllText(@"appsettings.Development.json");
            AppConfiguration config = JsonConvert.DeserializeObject<AppConfiguration>(jsonString);
            return config.TestDatabase.ConnectionString;
        }
    }
}