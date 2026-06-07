using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalBootstrapper : MonoBehaviour
{
    [Header("Системы (Префабы)")]
    [SerializeField] private UnityAudioService _audioServicePrefab;
    [SerializeField] private UnityVfxService _vfxServicePrefab;

    private void Awake()
    {
        Debug.Log("Запуск движка: Инициализация глобальных сервисов...");
        InitializeServices();
        // тут можно использовать LoadSceneAsync для прогресс-бара
        LoadGameScene();
    }

    private void InitializeServices()
    {
        ServiceLocator.Clear();

        // === СИСТЕМА СОХРАНЕНИЙ ===
        ISaveSystem saveSystem = new DummySaveSystem();
        ServiceLocator.Register<ISaveSystem>(saveSystem);

        // === АУДИО СИСТЕМА ===
        if (_audioServicePrefab != null)
        {
            UnityAudioService audioInstance = Instantiate(_audioServicePrefab);
            DontDestroyOnLoad(audioInstance.gameObject);
            ServiceLocator.Register<IAudioService>(audioInstance);
        }

        // === VFX СИСТЕМА ===
        if (_vfxServicePrefab != null)
        {
            UnityVfxService vfxInstance = Instantiate(_vfxServicePrefab);
            DontDestroyOnLoad(vfxInstance.gameObject);
            ServiceLocator.Register<IVfxService>(vfxInstance);
        }
    }

    private void LoadGameScene()
    {
        Debug.Log("Сервисы зарегистрированы. Загрузка GameScene.");
        SceneManager.LoadScene("GameScene");
    }
}