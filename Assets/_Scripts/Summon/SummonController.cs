
using UnityEngine;

public class SummonController
{
    private GridModel _gridModel;
    private UnitData _baseUnitToSummon;
    private IWalletService _wallet;

    private int _summonCost = 10;
    public SummonController(GridModel gridModel, SummonPanelView summonView, UnitData baseUnit, IWalletService wallet)
    {
        _gridModel = gridModel;
        _baseUnitToSummon = baseUnit;
        _wallet = wallet;
        summonView.OnSummonButtonClicked += HandleSummonRequest;
    }

    private void HandleSummonRequest()
    {
        if (_gridModel.TryGetFreeSlot(out int x, out int y))
        {
            if (_wallet.TrySpend(_summonCost))
            {
                _gridModel.PlaceUnit(x, y, _baseUnitToSummon);
                Debug.Log($"<color=magenta>[Котел]</color> Призван новый юнит на клетку [{x},{y}]!");
            }
            else
            {
                Debug.Log("<color=red>[Котел]</color> Провал! Не хватает душ.");
            }
        }
        else
        {
            Debug.Log("<color=red>[Котел]</color> Провал! На доске нет свободного места.");
        }
    }
}
