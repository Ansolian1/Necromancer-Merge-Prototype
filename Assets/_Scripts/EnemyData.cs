using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy", order = 0)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField, TextArea(3, 5)] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _baseHp;
    [SerializeField] private int _damage;
    [SerializeField] private int _dropGold;

    public string UnitName => _enemyName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public int BaseHp => _baseHp;
    public int Damage => _damage;
    public int DropGold => _dropGold;
}
