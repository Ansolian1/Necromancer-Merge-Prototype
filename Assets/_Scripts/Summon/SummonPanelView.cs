using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonPanelView : MonoBehaviour
{
    [SerializeField] private Button _summonButton;
    [SerializeField] private TextMeshProUGUI _summonButtonText;

    public event Action OnSummonButtonClicked;

    private void Awake()
    {
        if (_summonButton != null)
        {
            _summonButton.onClick.AddListener(() => OnSummonButtonClicked?.Invoke());
        }
    }
    public void SetupButtonText(UnitData unit)
    {
        _summonButtonText.text = $"Призвать ({unit.Cost})";
    }
}
