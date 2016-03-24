// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonParser.cs" company="">
//   
// </copyright>
// <summary>
//   The json parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynoDs.Core.JsonParser
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using SynoDs.Core.Contracts;
    using SynoDs.Core.Dal.HttpBase;
    using SynoDs.Core.Exceptions;

    /// <summary>
    /// The JSON parser.
    /// </summary>
    public class JsonParser : IJsonParser
    {
        /// <summary>
        /// The _error provider.
        /// </summary>
        private readonly IErrorProvider errorProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonParser"/> class.
        /// </summary>
        /// <param name="errorProvider">
        /// The error provider.
        /// </param>
        public JsonParser(IErrorProvider errorProvider)
        {
            this.errorProvider = errorProvider;
        }

        /// <summary>
        /// Tries to deserialize the json text into an Object.
        ///     This method will attempt to locate error data in the json string.
        ///     If no error is reported, we will continue to deserialize.
        ///     If an error is detected, it will throw an exception.
        /// </summary>
        /// <typeparam name="T">
        /// The DAL object that we're deserializing to.
        /// </typeparam>
        /// <param name="json">
        /// The JSON text.
        /// </param>
        /// <returns>
        /// The DAL object with the actual data.
        /// </returns>
        public T FromJson<T>(string json)
        {
            var obj = JObject.Parse(json);
            var success = (bool)obj["success"];

            if (success)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            var errorObject = obj["error"];
            if (errorObject == null)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            var error = errorObject.ToObject<ErrorObject>();

            if (error.Code == 0)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            if (error.Code == 400)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            var errorMessage = this.errorProvider.GetErrorDescriptionForType<T>(error.Code);
            throw new SynologyException(error.Code, errorMessage);
        }
    }
}