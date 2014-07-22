namespace SynoDs.Core.Interfaces
{
    public interface IParameterMapper
    {
        Dal.HttpBase.RequestParameters CreateRequestParameters<T1>();
    }
}