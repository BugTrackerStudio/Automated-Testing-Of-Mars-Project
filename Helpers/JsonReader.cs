using Newtonsoft.Json.Linq;
using System.IO;

namespace qa_dotnet_cucumber.Helpers
{
    public static class JsonReader
    {
        public static string GetValue(string fileName, string section, string key)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", $"{fileName}.json");
            var jsonText = File.ReadAllText(filePath);
            JObject jsonData = JObject.Parse(jsonText);

            // If the section is an array, take the first object
            var sectionData = jsonData[section];
            if (sectionData is JArray)
            {
                return sectionData[0][key]?.ToString();
            }
            else
            {
                return sectionData[key]?.ToString();
            }
        }
    }
}
