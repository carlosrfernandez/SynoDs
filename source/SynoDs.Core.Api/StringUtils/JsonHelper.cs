using Newtonsoft.Json;
using SynoDs.Core.Interfaces;

namespace SynoDs.Core.Api.StringUtils
{
    public class JsonHandler : IJsonParser
    {
        //ILoggingProvider Logger { get; set; }
        //IExceptionHandler ExceptionHandler { get; set; }

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
