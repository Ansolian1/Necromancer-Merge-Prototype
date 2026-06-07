using System;
using UnityEngine;
using UnityEngine.UI;

public class SummonPanelView : MonoBehaviour
{
    [SerializeField] private Button _summonButton;

    public event Action OnSummonButtonClicked;

    private void Awake()
    {
        if (_summonButton != null)
        {
            _summonButton.onClick.AddListener(() => OnSummonButtonClicked?.Invoke());
        }
    }
}
