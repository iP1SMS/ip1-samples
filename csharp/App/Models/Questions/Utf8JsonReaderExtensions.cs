using System.Collections.Generic;
using System.Text.Json;

namespace IP1.Samples.Models
{
    public static class Utf8JsonReaderExtensions
    {
        public static Dictionary<string, Dictionary<string, string>> GetTranslations(this ref Utf8JsonReader reader, JsonSerializerOptions options) => reader.GetObject<Dictionary<string, Dictionary<string, string>>>(options);

        public static Coords GetCoords(this ref Utf8JsonReader reader, JsonSerializerOptions options) => reader.GetObject<Coords>(options);

        public static List<T> GetAlternatives<T>(this ref Utf8JsonReader reader, JsonSerializerOptions options) where T : IQuestionAlternative, new() => reader.GetArray<T>(options);
    }
}
