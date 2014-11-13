namespace SynoDs.Core.JsonParser
{
    using Newtonsoft.Json;
    using Interfaces;
    using Newtonsoft.Json.Linq;
    using Exception;

    public class JsonParser : IJsonParser
    {
        private readonly IErrorProvider _errorProvider;

        public JsonParser(IErrorProvider errorProvider)
        {
            _errorProvider = errorProvider;
        }

        public string ToJson<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance);    
        }

        public T FromJson<T>(string json)
        {
            var obj = JObject.Parse(json);
            var errorCode = (int)obj["error"];
            var success = (bool)obj["success"];
            if (success && errorCode == 0)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            //TODO: Implement calling API for error handling.
            var errorMessage = _errorProvider.GetErrorDescriptionForCode("", errorCode);
            throw new SynologyException(errorMessage);
        }
    }
}
