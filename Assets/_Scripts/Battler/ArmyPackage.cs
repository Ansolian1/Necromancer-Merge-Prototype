using System.Collections.Generic;

[System.Serializable]
public struct PackagedUnit
{
    public UnitData Data;
    public int GridX;
    public int GridY;
}

public class ArmyPackage
{
    public List<PackagedUnit> AliveUnits = new List<PackagedUnit>();
    public int TotalArmyPower => AliveUnits.Count;
}
