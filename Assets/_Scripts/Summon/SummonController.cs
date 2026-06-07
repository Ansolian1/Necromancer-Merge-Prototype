
using UnityEngine;

public class SummonController
{
    private GridModel _gridModel;
    private UnitData _baseUnitToSummon;

    public SummonController(GridModel gridModel, SummonPanelView summonView, UnitData baseUnit)
    {
        _gridModel = gridModel;
        _baseUnitToSummon = baseUnit;

        summonView.OnSummonButtonClicked += HandleSummonRequest;
    }

    private void HandleSummonRequest()
    {
        if (_gridModel.TryGetFreeSlot(out int x, out int y))
        {
            _gridModel.PlaceUnit(x, y, _baseUnitToSummon);
            Debug.Log($"<color=magenta>[Котел]</color> Призван новый юнит на клетку [{x},{y}]!");
        }
        else
        {
            Debug.Log("<color=red>[Котел]</color> Провал! На доске нет свободного места.");
        }
    }
}
