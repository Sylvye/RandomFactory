using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileActionController : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private Tilemap overlayTilemap;
    [SerializeField] private TileBase validOutlineTile;
    [SerializeField] private TileBase invalidOutlineTile;
    [SerializeField] private TileBase placeholderCraftingTile;

    private PlayerController _player;
    private PlayerAbilityManager _pam;
    private TileBase _selectedPlaceable;
    private Vector3Int? _outlinedCell;
    private Vector3Int? _hoveredCell;
    private bool _hoveredCellIsValid;

    public TileBase SelectedPlaceable => _selectedPlaceable;
    public Vector3Int? HoveredCell => _hoveredCell;
    public bool HoveredCellIsValid => _hoveredCellIsValid;

    private void Start()
    {
        worldGenerator = WorldGenerator.Main;
        _player = PlayerController.Main;
    }

    private void Update()
    {
        HandlePlaceholderSelection();

        var pointer = PointerInputManager.Main;
        if (pointer is null || worldGenerator is null || overlayTilemap is null || _player is null)
        {
            ClearHover();
            return;
        }

        var pointerOverUi = EventSystem.current is not null && EventSystem.current.IsPointerOverGameObject();
        RefreshHover(pointer.GetMouseWorldPos(), pointerOverUi);

        if (!pointerOverUi && pointer.WasLeftButtonPressedThisFrame())
        {
            ExecuteHoveredAction();
        }
    }

    private void OnDisable()
    {
        ClearHover();
    }

    public void SelectPlaceable(TileBase tile)
    {
        _selectedPlaceable = tile;
    }

    public void ClearSelection()
    {
        _selectedPlaceable = null;
    }

    public void RefreshHover(Vector2 worldPosition, bool pointerOverUi = false)
    {
        ClearOutline();
        _hoveredCell = null;
        _hoveredCellIsValid = false;

        if (pointerOverUi || worldGenerator is null || overlayTilemap is null || _player is null) return;

        var cellPosition = worldGenerator.SolidTilemap.WorldToCell(worldPosition);
        if (!worldGenerator.TryGetCell(cellPosition, out _)) return;

        _hoveredCell = cellPosition;
        _hoveredCellIsValid = EvaluateCell(cellPosition);
        overlayTilemap.SetTile(cellPosition, _hoveredCellIsValid ? validOutlineTile : invalidOutlineTile);
        _outlinedCell = cellPosition;
    }

    public bool ExecuteHoveredAction()
    {
        if (!_hoveredCell.HasValue) return false;

        var cellPosition = _hoveredCell.Value;
        _hoveredCellIsValid = EvaluateCell(cellPosition);
        if (!_hoveredCellIsValid)
        {
            RefreshOutline(cellPosition, false);
            return false;
        }

        if (_selectedPlaceable is not null)
        {
            var placed = worldGenerator.TryPlaceTile(_selectedPlaceable, cellPosition);
            RefreshOutline(cellPosition, EvaluateCell(cellPosition));
            return placed;
        }

        var interactable = GetInteractable(cellPosition);
        if (interactable is null || !interactable.CanInteract(_player))
        {
            RefreshOutline(cellPosition, false);
            return false;
        }

        interactable.OnInteract(_player);
        RefreshOutline(cellPosition, EvaluateCell(cellPosition));
        return true;
    }

    private bool EvaluateCell(Vector3Int cellPosition)
    {
        var center = worldGenerator.GetCellCenterWorld(cellPosition);
        if (!_pam.IsWithinReach(center)) return false;

        if (_selectedPlaceable is not null)
        {
            return worldGenerator.CanPlaceTile(_selectedPlaceable, cellPosition);
        }

        var interactable = GetInteractable(cellPosition);
        return interactable is not null && interactable.CanInteract(_player);
    }

    private IInteractable GetInteractable(Vector3Int cellPosition)
    {
        var tileObject = worldGenerator.GetTileObject(cellPosition);
        return tileObject is null ? null : tileObject.GetComponentInChildren<IInteractable>();
    }

    private void HandlePlaceholderSelection()
    {
        var keyboard = Keyboard.current;
        if (keyboard is null) return;

        if (keyboard.digit1Key.wasPressedThisFrame)
        {
            SelectPlaceable(placeholderCraftingTile);
        }
        else if (keyboard.escapeKey.wasPressedThisFrame)
        {
            ClearSelection();
        }
    }

    private void RefreshOutline(Vector3Int cellPosition, bool isValid)
    {
        ClearOutline();
        _hoveredCellIsValid = isValid;
        overlayTilemap.SetTile(cellPosition, isValid ? validOutlineTile : invalidOutlineTile);
        _outlinedCell = cellPosition;
    }

    private void ClearHover()
    {
        ClearOutline();
        _hoveredCell = null;
        _hoveredCellIsValid = false;
    }

    private void ClearOutline()
    {
        if (_outlinedCell.HasValue && overlayTilemap is not null)
        {
            overlayTilemap.SetTile(_outlinedCell.Value, null);
        }

        _outlinedCell = null;
    }
}
