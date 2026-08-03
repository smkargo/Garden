using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Grid")]
    public Vector2Int GridPosition { get; private set; }

    [Header("Visuals")]
    [SerializeField] protected SpriteRenderer soilRenderer;
    [SerializeField] protected SpriteRenderer grassRenderer;

    public virtual void Initialize(Vector2Int position)
    {
        GridPosition = position;
    }
}