
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

    [Header("Тестовые Данные (SO)")]
    [SerializeField] private UnitData _testSkeleton;

    private GridModel _gridModel;
    private GridController _gridController;
    private SummonController _summonController;

    private async void Start()
    {
        // ЗАЩИТА ОТ ДУРАКА: Если запустили GameScene напрямую, минуя InitScene
        if (!IsServiceLocatorReady())
            return;

        Debug.Log("[GameBoot] Фаза 1: Получение глобальных сервисов...");
        var saveSystem = ServiceLocator.Get<ISaveSystem>();
        var audioSystem = ServiceLocator.Get<IAudioService>();
        var vfxSystem = ServiceLocator.Get<IVfxService>();
        var walletSystem = ServiceLocator.Get<IWalletService>();
        Debug.Log("[GameBoot] Фаза 2: Асинхронная загрузка сохранений уровня...");
        // В реальной игре мы заменим тип `object` на наш класс сохранения, например GameSaveData
        // object saveData = await saveSystem.LoadAsync<object>("main_save_key");


        Debug.Log("[GameBoot] Фаза 3: Инициализация Моделей (Чистые данные)... ");
        //Тут будет передача из сохранений в модели
        _gridModel = new GridModel();

        Debug.Log("[GameBoot] Фаза 4: Инициализация Контроллеров (Связка Моделей и View)...");
        _gridController = new GridController(_gridModel, _gridView, _trashZoneView, audioSystem, vfxSystem, walletSystem);
        _summonController = new SummonController(_gridModel, _summonView, _testSkeleton, walletSystem);

        _battleTransitionView.Init(_gridModel);

        Debug.Log("[GameBoot] Фаза 5: Отрисовка стартового состояния и старт игры...");
        _gridController.InitializeTopToBottom(); 

        Debug.Log("<color=green>[GameBoot] Сцена успешно инициализирована!</color>");
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
