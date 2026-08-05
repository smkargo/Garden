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
    [SerializeField] private Animator grassAnimator;
    [SerializeField] private ParticleSystem grassEffect;
    [SerializeField] private SpriteRenderer[] flowerRenderers;
    [SerializeField] private Animator[] flowerAnimators;

private int activeFlowerIndex = -1;


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
       foreach (var flower in flowerRenderers)
        {
            if (flower != null)
                flower.enabled = false;
        }
    }

public virtual void Fill(int regionId)
{
    RegionId = regionId;
    State = TileState.Filled;

    if (grassRenderer == null)
        return;

    grassRenderer.enabled = true;
    AudioManager.Instance.PlayGrass();
    foreach (var flower in flowerRenderers)
        {
            if (flower != null)
                flower.enabled = false;
        }

   if (grassAnimator != null)
    {
        grassAnimator.Rebind();
        grassAnimator.Update(0f);
        grassAnimator.Play("GrassGrow", 0, 0f);
    }

    if (grassEffect != null)
    {
        grassEffect.Play();
    }
}
    public virtual void ClearFill()
    {
        RegionId = -1;
        State = TileState.Empty;

       if (grassRenderer != null)
        grassRenderer.enabled = false;
    
    foreach (var flower in flowerRenderers)
    {
        if (flower != null)
            flower.enabled = false;
    }
    }

public virtual void Complete(int flowerType)
{
    State = TileState.Completed;

    if (grassRenderer != null)
        grassRenderer.enabled = false;

    foreach (var flower in flowerRenderers)
        flower.enabled = false;

    activeFlowerIndex = flowerType;

    flowerRenderers[activeFlowerIndex].enabled = true;
    AudioManager.Instance.PlayFlower();

    if (flowerAnimators[activeFlowerIndex] != null)
    {
        flowerAnimators[activeFlowerIndex].Rebind();
        flowerAnimators[activeFlowerIndex].Update(0f);
        flowerAnimators[activeFlowerIndex].Play("FlowerBloom", 0, 0f);
    }
}
}