namespace SynoDs.Core.JsonParser
{
    using Newtonsoft.Json;
    using Interfaces;

    public class JsonParser : IJsonParser
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
                //TODO: add error handling.
                return default(T);
            }
        }
    }
}
