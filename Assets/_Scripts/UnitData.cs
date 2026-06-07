using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "SO/Unit", order = 0)]
public class UnitData : ScriptableObject
{
    [Header("Звук при создании этого мутанта")]
    public AudioClip MergeSound;

    [SerializeField] private string _unitName;
    [SerializeField, TextArea(3, 5)] private string _description;
    [SerializeField] private Sprite _icon;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _baseHp;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private int _reward;
    [SerializeField] private int _sacrificeValue;
    [SerializeField] private UnitData _nextTierUnit;

    public string UnitName => _unitName;
    public string Description => _description;
    public Sprite Icon => _icon;
    public GameObject Prefab => _prefab;
    public int BaseHp => _baseHp;
    public int Cost => _cost;
    public int Reward => _reward;
    public int Damage => _damage;
    public int SacrificeValue => _sacrificeValue;
    public UnitData NextTierUnit => _nextTierUnit;
}
