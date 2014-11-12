using SynoDs.Core.Dal.HttpBase;

namespace SynoDs.Core.Interfaces
{
    public interface IJsonParser
    {
        //todo find a place for error provider.
        IErrorProvider ErrorProvider { get; set; } // we might need to overwrite this property at runtime.
        string ToJson<T>(T instance);
        T FromJson<T>(string json);

    }
}
