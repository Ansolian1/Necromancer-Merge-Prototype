using System;
using UnityEngine;
using UnityEngine.UI;

public class GridView : MonoBehaviour
{
    [SerializeField] private CellView[] _flatCells;
    [SerializeField] private int _width = 3;

    [Header("UI Призрака")]
    [SerializeField] private Image _dragGhostImage;

    private CellView[,] _uiSlots;
    private CellView _draggedCell;
    public event Action<int, int, int, int> OnPlayerDraggedSlot;

    private void Awake()
    {
        int height = _flatCells.Length / _width;
        _uiSlots = new CellView[_width, height];

        int index = 0;
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < _width; x++)
            {
                CellView cell = _flatCells[index];
                _uiSlots[x, y] = cell;
                cell.Initialize(x, y);
                cell.OnDragStarted += HandleDragStart;
                cell.OnDragging += HandleDragging;
                cell.OnDroppedOver += HandleDrop;
                cell.OnDragEnded += HandleDragEnd;
                index++;
            }
        }
    }

    private void HandleDragStart(CellView cell)
    {
        _draggedCell = cell;
        cell.SetFaded(true);
        _dragGhostImage.gameObject.SetActive(true);
        _dragGhostImage.sprite = cell.GetSprite();
    }
    private void HandleDragging(Vector2 mousePosition)
    {
        if (_dragGhostImage.gameObject.activeSelf)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                _dragGhostImage.rectTransform,
                mousePosition,
                Camera.main,
                out Vector3 worldPoint);

            _dragGhostImage.transform.position = worldPoint;
        }
    }
    private void HandleDrop(CellView dropTargetCell)
    {
        if (_draggedCell != null)
        {
            _dragGhostImage.gameObject.SetActive(false);
            _draggedCell.SetFaded(false);
            if (_draggedCell != dropTargetCell)
            {
                OnPlayerDraggedSlot?.Invoke(_draggedCell.X, _draggedCell.Y, dropTargetCell.X, dropTargetCell.Y);
            }
            _draggedCell = null;
        }
    }

    private void HandleDragEnd()
    {
        _dragGhostImage.gameObject.SetActive(false);
    }

    public void UpdateUISlot(int x, int y, Sprite icon)
    {
        _uiSlots[x, y].SetIcon(icon);
    }

    public void PlayMergeJuiceAt(int x, int y)
    {
        _uiSlots[x, y].PlayMergeJuice(); // Вызываем DOTween из CellView
    }

    /// <summary>
    /// Возвращает физические координаты центра ячейки в мире для спавна Партиклов.
    /// </summary>
    public Vector3 GetWorldPositionOfCell(int x, int y)
    {
        if (x < 0 || x >= _width || y < 0 || y >= _uiSlots.GetLength(1))
        {
            Debug.LogError($"[GridView] Попытка получить координаты несуществующей ячейки: {x},{y}");
            return Vector3.zero;
        }
        return _uiSlots[x, y].transform.position;
    }
}
