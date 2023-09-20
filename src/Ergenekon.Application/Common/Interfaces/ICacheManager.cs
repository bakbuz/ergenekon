namespace Ergenekon.Application.Common.Interfaces;

public interface ICacheManager
{
    Task<T> GetAsync<T>(string key, Func<T> value);
}
