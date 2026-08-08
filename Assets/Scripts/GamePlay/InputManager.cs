using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    [SerializeField] private BoardRaycaster raycaster;
    [SerializeField] private RegionManager regionManager;
    [SerializeField] private RegionController controller;
    [SerializeField] private TutorialManager tutorialManager;
    private GameInput input;
    private void Awake()
{
    input = new GameInput();
}
private void OnEnable()
{
    input.Enable();
}
private void OnDisable()
{
    input.Disable();
}
void Update()
{
    if (GameManager.Instance.IsGameCompleted)
    return;
    if (input.Gameplay.Press.WasPressedThisFrame())
    {
        BeginInput();
    }

    if (controller.HasActiveRegion &&
        input.Gameplay.Press.IsPressed())
    {
        ContinueInput();
    }

    if (input.Gameplay.Press.WasReleasedThisFrame())
    {
        controller.EndRegion();

        if (tutorialManager != null)
        {
            tutorialManager.OnPlayerReleased();
        }
    }
}
void BeginInput()
{
    Vector2 pointerPosition =
        input.Gameplay.Point.ReadValue<Vector2>();

    BoardCell cell =
        raycaster.GetCellUnderPointer(
            pointerPosition
        );

    if (cell == null)
        return;

    if (cell.Tile is not NumberTile numberTile)
        return;

    Region region =
        regionManager.GetRegion(numberTile);

    if (region == null)
        return;

    controller.BeginRegion(region);

    if (tutorialManager != null)
    {
        tutorialManager.OnNumberTilePressed(
            numberTile
        );
    }
}
void ContinueInput()
{
    Vector2 pointer =
        input.Gameplay.Point.ReadValue<Vector2>();

    BoardCell cell =
        raycaster.GetCellUnderPointer(pointer);

    if (cell == null)
        return;

    controller.TryAddTile(cell.Tile);

    if (tutorialManager != null)
    {
        tutorialManager.OnTutorialTileEntered(
            cell.Tile
        );
    }
}
}