using System;

public class GridModel
{
    private UnitData[,] _gridState = new UnitData[3, 3];
    public event Action<int, int, UnitData> OnGridChanged;
    public event Action<int, int, UnitData> OnMergeSuccess;

    public UnitData GetUnitAt(int x, int y)
    {
        return _gridState[x, y];
    }
    public void PlaceUnit(int x, int y, UnitData unit)
    {
        _gridState[x, y] = unit;
        OnGridChanged?.Invoke(x, y, unit);
    }

    public void ClearCell(int x, int y)
    {
        _gridState[x, y] = null;
        OnGridChanged?.Invoke(x, y, null);
    }

    public void TryMoveOrMerge(int fromX, int fromY, int toX, int toY)
    {
        if (fromX == toX && fromY == toY) return;

        UnitData unitA = _gridState[fromX, fromY];
        UnitData unitB = _gridState[toX, toY];
       
        if (unitA == null) return;

        if (unitB == null)
        {
            _gridState[toX, toY] = unitA;
            _gridState[fromX, fromY] = null;
            OnGridChanged?.Invoke(toX, toY, unitA);
            OnGridChanged?.Invoke(fromX, fromY, null);
            return;
        }

        if (unitA == unitB && unitA.NextTierUnit != null)
        {
            _gridState[toX, toY] = unitA.NextTierUnit;
            _gridState[fromX, fromY] = null;

            OnGridChanged?.Invoke(toX, toY, _gridState[toX, toY]);
            OnGridChanged?.Invoke(fromX, fromY, null);
            OnMergeSuccess?.Invoke(toX, toY, _gridState[toX, toY]);
        }

        if (unitA != unitB)
        {
            _gridState[toX, toY] = unitA;
            _gridState[fromX, fromY] = unitB;

            OnGridChanged?.Invoke(toX, toY, unitA);
            OnGridChanged?.Invoke(fromX, fromY, unitB);
        }
    }
    public bool TryGetFreeSlot(out int freeX, out int freeY)
    {
        for (int y = 0; y < _gridState.GetLength(1); y++)
        {
            for (int x = 0; x < _gridState.GetLength(0); x++)
            {
                if (_gridState[x, y] == null)
                {
                    freeX = x;
                    freeY = y;
                    return true;
                }
            }
        }

        freeX = -1;
        freeY = -1;
        return false;
    }
    /// <summary>
    /// Запаковывает всю доску в DTO (Посылку) для отправки в Автобатлер.
    /// Пустые клетки игнорируются.
    /// </summary>
    public ArmyPackage ExportArmy()
    {
        ArmyPackage package = new ArmyPackage();
        for (int x = 0; x < _gridState.GetLength(0); x++)
        {
            for (int y = 0; y < _gridState.GetLength(1); y++)
            {
                UnitData unit = _gridState[x, y];
                if (unit != null)
                {
                    PackagedUnit packagedUnit = new PackagedUnit
                    {
                        Data = unit,
                        GridX = x,
                        GridY = y
                    };
                    package.AliveUnits.Add(packagedUnit);
                }
            }
        }
        return package;
    }
}
