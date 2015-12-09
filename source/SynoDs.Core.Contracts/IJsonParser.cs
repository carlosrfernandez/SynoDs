namespace SynoDs.Core.Contracts
{
    public interface IJsonParser
    {
        // string ToJson<T>(T instance);
        /// <summary>
        /// The from json.
        /// </summary>
        /// <param name="json">
        /// The json.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T FromJson<T>(string json);
    }
}
