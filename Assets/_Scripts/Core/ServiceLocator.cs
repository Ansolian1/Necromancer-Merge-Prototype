using System;
using System.Collections.Generic;
/// <summary>
/// Глобальный реестр сервисов.
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    /// <summary>
    /// Положить сервис на полку.
    /// </summary>
    public static void Register<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            throw new Exception($"Сервис типа {type} уже зарегистрирован!");
        }
        _services[type] = service;
    }

    /// <summary>
    /// Достать сервис с полки.
    /// </summary>
    public static T Get<T>()
    {
        var type = typeof(T);
        if (!_services.TryGetValue(type, out var service))
        {
            throw new Exception($"Попытка получить незарегистрированный сервис {type}!");
        }
        return (T)service;
    }

    /// <summary>
    /// Очистить шкаф.
    /// </summary>
    public static void Clear()
    {
        _services.Clear();
    }
}
