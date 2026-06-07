using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class GridController
{
    private GridModel _model;
    private GridView _view;
    private TrashZoneView _trashZone;
    private IAudioService _audio;
    private IVfxService _vfxSystem;
    private IWalletService _wallet;
    private FloatingText _floatingTextPrefab;
    public GridController(GridModel model, GridView view, TrashZoneView trashZone, IAudioService audio, IVfxService vfxSystem, IWalletService walletSystem, FloatingText floatingTextPrefab)
    {
        _model = model;
        _view = view;
        _audio = audio;
        _vfxSystem = vfxSystem;
        _wallet = walletSystem;
        _trashZone = trashZone;
        _view.OnPlayerDraggedSlot += HandlePlayerInput;
        _model.OnGridChanged += UpdateView;
        _model.OnMergeSuccess += HandleMergeSuccess;
        _trashZone.OnUnitSacrificed += HandleSacrifice;
        _floatingTextPrefab = floatingTextPrefab;
    }
    private void HandleSacrifice(CellView sacrificedCell)
    {
        int x = sacrificedCell.X;
        int y = sacrificedCell.Y;

        UnitData unit = _model.GetUnitAt(x, y);

        if (unit != null)
        {
            _model.ClearCell(x, y);
            _wallet.Add(unit.SacrificeValue);
            _vfxSystem.PlayMergeVfx(_trashZone.transform.position);
        }
    }
    private void HandlePlayerInput(int fromX, int fromY, int toX, int toY)
    {
        _model.TryMoveOrMerge(fromX, fromY, toX, toY);
    }

    private void UpdateView(int x, int y, UnitData data)
    {
        Sprite targetIcon = data != null ? data.Icon : null;
        _view.UpdateUISlot(x, y, targetIcon);
    }
    private void HandleMergeSuccess(int x, int y, UnitData data)
    {
        _view.PlayMergeJuiceAt(x, y);
        Vector3 worldPos = _view.GetWorldPositionOfCell(x, y);
        _vfxSystem.PlayMergeVfx(worldPos);
        if (data.MergeSound != null)
        {
            _audio.PlaySFX(data.MergeSound);
        }
        FloatingText popup = Object.Instantiate(_floatingTextPrefab, worldPos, Quaternion.identity, _view.transform);
        popup.Animate($"+{data.Reward}", Color.green); // +5 летит вверх!
        _wallet.Add(data.Reward);
    }



    public void InitializeTopToBottom()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                UnitData unit = _model.GetUnitAt(x, y);
                _view.UpdateUISlot(x, y, unit != null ? unit.Icon : null);
            }
        }
    }
}

