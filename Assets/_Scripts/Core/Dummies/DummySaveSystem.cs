using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DummySaveSystem : ISaveSystem
{
    private readonly Dictionary<string, object> _memoryStorage = new Dictionary<string, object>();

    public void Save<T>(string key, T data)
    {
        Debug.Log($"[DummySave] Сейф '{key}' сохранен в RAM.");
        _memoryStorage[key] = data;
    }

    public Task<T> LoadAsync<T>(string key)
    {
        if (_memoryStorage.TryGetValue(key, out object savedData))
        {
            Debug.Log($"[DummySave] Сейф '{key}' успешно загружен из RAM.");
            return Task.FromResult((T)savedData);
        }

        Debug.LogWarning($"[DummySave] Сейф '{key}' не найден. Возвращаем default.");
        return Task.FromResult(default(T));
    }

    public bool HasSave(string key)
    {
        return _memoryStorage.ContainsKey(key);
    }

    public void DeleteSave(string key)
    {
        if (_memoryStorage.ContainsKey(key))
        {
            _memoryStorage.Remove(key);
            Debug.Log($"[DummySave] Сейф '{key}' удален.");
        }
    }
}
