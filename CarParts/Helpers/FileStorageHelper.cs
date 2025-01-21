using System.Text.Json;

namespace CarParts.Helpers
{
    public static class FileStorageHelper
    {
        public static List<T> ReadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();

            var json = File.ReadAllText(filePath);

            return string.IsNullOrWhiteSpace(json)
                ? new List<T>()
                : JsonSerializer.Deserialize<List<T>>(json);
        }

        public static void WriteToFile<T>(string filePath, List<T> data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, json);
        }
    }
}
