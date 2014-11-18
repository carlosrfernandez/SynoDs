namespace SynoDs.Core.Contracts
{
    public interface IJsonParser
    {
        // string ToJson<T>(T instance);
        T FromJson<T>(string json);
    }
}
