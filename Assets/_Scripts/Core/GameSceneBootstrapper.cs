
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Единая точка композиции для игровой сцены (Composition Root).
/// Собирает MVC составляющие и запускает уровень.
/// </summary>
public class GameSceneBootstrapper : MonoBehaviour
{
    [Header("Сюда перетаскивать UI и View скрипты сцены")]
    [Space(10)]
    [SerializeField] private GridView _gridView;
    [SerializeField] private SummonPanelView _summonView;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private TrashZoneView _trashZoneView;
    [SerializeField] private BattleTransitionView _battleTransitionView;
    [SerializeField] private FloatingText _floatingText;

    [Header("Тестовые Данные (SO)")]
    [SerializeField] private UnitData _testSkeleton;

    private GridModel _gridModel;
    private GridController _gridController;
    private SummonController _summonController;

    private async void Start()
    {
        if (!IsServiceLocatorReady())
            return;

        Debug.Log("Получение глобальных сервисов...");
        var saveSystem = ServiceLocator.Get<ISaveSystem>();
        var audioSystem = ServiceLocator.Get<IAudioService>();
        var vfxSystem = ServiceLocator.Get<IVfxService>();
        var walletSystem = ServiceLocator.Get<IWalletService>();
        Debug.Log("Асинхронная загрузка сохранений уровня...");
        // object saveData = await saveSystem.LoadAsync<object>("main_save_key");


        Debug.Log("Инициализация Моделей");
        //Тут будет передача из сохранений в модели
        _gridModel = new GridModel();

        Debug.Log("Инициализация Контроллеров (Связка Моделей и View)...");
        _gridController = new GridController(_gridModel, _gridView, _trashZoneView, audioSystem, vfxSystem, walletSystem, _floatingText);
        _summonController = new SummonController(_gridModel, _summonView, _testSkeleton, walletSystem);

        _battleTransitionView.Init(_gridModel);

        Debug.Log("Отрисовка стартового состояния и старт игры...");
        _gridController.InitializeTopToBottom(); 

        Debug.Log("<color=green>[GameBoot] Сцена успешно инициализирована</color>");
    }

    /// <summary>
    /// Проверяет, запущены ли базовые системы. 
    /// </summary>
    private bool IsServiceLocatorReady()
    {
        try
        {
            ServiceLocator.Get<ISaveSystem>();
            return true;
        }
        catch
        {
            Debug.LogError("<b>Ошибка старта:</b> Сервисы не найдены! Вы забыли запустить игру через 'InitScene'.");
            return false;
        }
    }
}
