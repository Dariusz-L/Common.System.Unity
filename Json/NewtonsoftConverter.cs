using Newtonsoft.Json;

namespace Common.Basic.Json
{
    public class NewtonsoftJsonConverter : IJsonConverter
    {

        T IJsonConverter.Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        string IJsonConverter.Serialize<T>(T obj)
        {
            var jsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

            return JsonConvert.SerializeObject(obj, jsonSettings);
        }
    }
}
