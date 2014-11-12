using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Interfaces
{
    public interface IJsonParser
    {
        IErrorProvider ErrorProvider { get; }
        string ToJson<T>(T instance);
        T FromJson<T>(string json);

    }
}
