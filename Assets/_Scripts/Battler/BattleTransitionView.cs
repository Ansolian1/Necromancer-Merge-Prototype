using UnityEngine;
using UnityEngine.UI;

public class BattleTransitionView : MonoBehaviour
{
    [SerializeField] private Button _startBattleButton;
    private GridModel _gridModel;

    public void Init(GridModel gridModel)
    {
        _gridModel = gridModel;
        _startBattleButton.onClick.AddListener(SendArmyToBattle);
    }

    private void SendArmyToBattle()
    {
        ArmyPackage myArmy = _gridModel.ExportArmy();
        Debug.Log($"<color=orange>[Почта Некроманта]</color> Сформирована армия! Всего бойцов: {myArmy.TotalArmyPower}");
        foreach (var soldier in myArmy.AliveUnits)
        {
            Debug.Log($"-> {soldier.Data.name} поедет на позицию [X:{soldier.GridX}, Y:{soldier.GridY}]");
        }

        // В БУДУЩЕМ: 
        // SceneManager.LoadScene("BattleScene");
        // ИЛИ BattleManager.StartBattle(myArmy);
    }
}