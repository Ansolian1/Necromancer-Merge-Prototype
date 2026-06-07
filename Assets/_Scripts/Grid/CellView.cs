
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public int X { get; private set; }
    public int Y { get; private set; }

    [Header("UI Ссылки")]
    [SerializeField] private Image _iconImage;
    [Tooltip("Объект-пустышка между картинкой и ячейкой.")]
    [SerializeField] private RectTransform _visualPivot;

    [Header("Сок (Juice)")]
    //[SerializeField] private ParticleSystem _vfxPrefab; // Раскомментируй, когда разберешься с партиклами
    [SerializeField] private float _animDuration = 0.4f;

    public event Action<CellView> OnDragStarted;
    public event Action<Vector2> OnDragging;
    public event Action<CellView> OnDroppedOver;

    public void Initialize(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
        _iconImage.enabled = icon != null;
    }
    public void SetFaded(bool isFaded)
    {
        var tempColor = _iconImage.color;
        tempColor.a = isFaded ? 0.3f : 1f;
        _iconImage.color = tempColor;
    }
    public Sprite GetSprite() => _iconImage.sprite;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_iconImage.sprite == null) return; // Пустую клетку не тащим!
        OnDragStarted?.Invoke(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragging?.Invoke(Input.mousePosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetFaded(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnDroppedOver?.Invoke(this);
    }

    /// <summary>
    /// Вызывается Контроллером, когда юнит появился в результате слияния.
    /// </summary>
    public void PlayMergeJuice()
    {
        DOTween.Kill(_visualPivot);
        _visualPivot.localScale = Vector3.zero;
        Sequence mergeSeq = DOTween.Sequence();
        mergeSeq.Append(_visualPivot.DOScale(new Vector3(1.2f, 1.2f, 1f), _animDuration * 0.4f).SetEase(Ease.OutQuad))
                .Append(_visualPivot.DOScale(Vector3.one, _animDuration * 0.6f).SetEase(Ease.OutBounce));
        // if (_vfxPrefab != null) { ... }
    }
}
