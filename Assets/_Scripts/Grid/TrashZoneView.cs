
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class TrashZoneView : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform _visualArea;

    public event Action<CellView> OnUnitSacrificed;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CellView droppedCell = eventData.pointerDrag.GetComponent<CellView>();
            if (droppedCell != null)
            {
                OnUnitSacrificed?.Invoke(droppedCell);
                _visualArea.DOPunchScale(new Vector3(0.3f, 0.3f, 0), 0.3f, 5, 0.5f);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            DOTween.Kill(_visualArea);
            _visualArea.DOScale(1.2f, 0.2f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(_visualArea);
        _visualArea.DOScale(1.0f, 0.2f);
    }
}
