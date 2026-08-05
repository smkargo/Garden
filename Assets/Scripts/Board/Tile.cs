using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Grid")]
    public Vector2Int GridPosition { get; private set; }

    [Header("Gameplay")]
    public TileState State { get; private set; } = TileState.Empty;

    public int RegionId { get; private set; } = -1;

    [Header("Visuals")]
    [SerializeField] protected SpriteRenderer soilRenderer;
    [SerializeField] protected SpriteRenderer grassRenderer;
    [SerializeField] private SpriteRenderer flowerRenderer;
    [SerializeField] private Animator grassAnimator;
    [SerializeField] private Animator flowerAnimator;
    [SerializeField] private ParticleSystem grassEffect;


    [Header("Border")]
    [SerializeField] private TileBorder border;

    public TileBorder Border => border;

    public virtual void Initialize(Vector2Int position)
    {
        GridPosition = position;

        State = TileState.Empty;
        RegionId = -1;

        if (border != null)
            border.HideAll();
        if (grassRenderer != null)
        {
            grassRenderer.enabled = false;
        }
        if (flowerRenderer != null)
        {
            flowerRenderer.enabled = false;
        }
    }

public virtual void Fill(int regionId)
{
    RegionId = regionId;
    State = TileState.Filled;

    if (grassRenderer == null)
        return;

    grassRenderer.enabled = true;
    flowerRenderer.enabled = false;

    if (grassAnimator != null)
    {
        grassAnimator.Rebind();
        grassAnimator.Update(0f);
        grassAnimator.Play("GrassGrow", 0, 0f);
        if (grassEffect != null)
        {
            grassEffect.Play();
        }
    }
}
    public virtual void ClearFill()
    {
        RegionId = -1;
        State = TileState.Empty;

       if (grassRenderer != null)
        grassRenderer.enabled = false;

    if (flowerRenderer != null)
        flowerRenderer.enabled = false;

    }

    public virtual void Complete()
    {
    
        grassRenderer.enabled = false;
        flowerRenderer.enabled = true;
        State = TileState.Completed;
        if (flowerAnimator != null)
    {
        flowerAnimator.Play("FlowerBloom", 0, 0f);
    }
    }
}