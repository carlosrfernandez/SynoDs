using SynoDs.Core.Dal.HttpBase;
using SynoDs.Core.Exceptions;

namespace SynoDs.Core.JsonParser
{
    using Newtonsoft.Json;
    using Contracts;
    using Newtonsoft.Json.Linq;

    public class JsonParser : IJsonParser
    {
        private readonly IErrorProvider _errorProvider;

        public JsonParser(IErrorProvider errorProvider)
        {
            _errorProvider = errorProvider;
        }

        /// <summary>
        /// Tries to deserialize the json text into an Object. 
        /// This method will attempt to locate error data in the json string.
        /// If no error is reported, we will continue to deserialize.
        /// If an error is detected, it will throw an exception. 
        /// </summary>
        /// <typeparam name="T">The DAL object that we're deserializing to.</typeparam>
        /// <param name="json">The JSON text.</param>
        /// <returns>The DAL object with the actual data.</returns>
        public T FromJson<T>(string json)
        {
            var obj = JObject.Parse(json);
            var success = (bool) obj["success"];

            if (success) return JsonConvert.DeserializeObject<T>(json);
            var errorObject = obj["error"];
            if (errorObject == null) return JsonConvert.DeserializeObject<T>(json);

            var error = errorObject.ToObject<ErrorObject>();

            if (error.Code == 0) return JsonConvert.DeserializeObject<T>(json);
            var errorMessage = _errorProvider.GetErrorDescriptionForType<T>(error.Code);
            throw new SynologyException(errorMessage);
        }
    }
}