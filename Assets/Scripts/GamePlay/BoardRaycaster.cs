using UnityEngine;

public class BoardRaycaster : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private BoardManager boardManager;

    public BoardCell GetCellUnderPointer(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (!hit)
            return null;

        Tile tile = hit.collider.GetComponent<Tile>();

        if (tile == null)
            return null;

        return boardManager.GetCell(tile.GridPosition);
    }
}