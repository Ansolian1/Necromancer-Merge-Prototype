using System.Threading.Tasks;

public interface ISaveSystem
{
    void Save<T>(string key, T data);
    Task<T> LoadAsync<T>(string key);
    bool HasSave(string key);
    void DeleteSave(string key);
}
