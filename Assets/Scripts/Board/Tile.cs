using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Grid")]
    public GridPosition GridPosition;

    [Header("State")]
    public bool IsPlayable = true;

    [Header("Visuals")]
    [SerializeField] protected SpriteRenderer soilRenderer;
    [SerializeField] protected SpriteRenderer grassRenderer;

    public virtual void Initialize(GridPosition position)
    {
        GridPosition = position;
    }
}