namespace SynoDs.Core.Api.StringUtils
{
    using Newtonsoft.Json;
    using Interfaces;

    public class JsonHandler : IJsonParser
    {
        public string ToJson<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance);
        }

        public T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
