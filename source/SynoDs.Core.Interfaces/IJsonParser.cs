using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Interfaces
{
    public interface IJsonParser
    {
        string ToJson<T>(T instance);
        T FromJson<T>(string json);
    }
}
