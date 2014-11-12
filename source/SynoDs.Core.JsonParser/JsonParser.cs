namespace SynoDs.Core.JsonParser
{
    using Newtonsoft.Json;
    using Interfaces;
    using Newtonsoft.Json.Linq;
    using Exception;


    public class JsonParser : IJsonParser
    {
        public IErrorProvider ErrorProvider { get; private set; }

        public JsonParser(IErrorProvider errorProvider)
        {
            ErrorProvider = errorProvider;
        }

        public string ToJson<T>(T instance)
        {
            return JsonConvert.SerializeObject(instance);    
        }

        public T FromJson<T>(string json)
        {
            try
            {
                var obj = JObject.Parse(json);
                var errorCode = (int)obj["error"];
                var success = (bool)obj["success"];
                if (success && errorCode == 0)
                {
                    return JsonConvert.DeserializeObject<T>(json);
                }
                var errorMessage = ErrorProvider.GetErrorDescriptionForCode(errorCode);
                throw new SynologyException(errorCode, errorMessage);
            }
            catch(System.Exception exception)
            {
                throw new System.Exception("Error while parsing the response from the Api.", exception);
            }
        }
    }
}
