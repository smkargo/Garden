using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ButterflyAnimation : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float frameRate = 0.12f;

    private SpriteRenderer spriteRenderer;

    private Sprite[] frames;
    private int currentFrame;
    private float timer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Sprite[] animationFrames)
    {
        if (animationFrames == null || animationFrames.Length == 0)
            return;

        frames = animationFrames;

        currentFrame = 0;
        timer = 0f;

        spriteRenderer.sprite = frames[0];
    }

    private void Update()
    {
        if (frames == null || frames.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer -= frameRate;

            currentFrame++;

            if (currentFrame >= frames.Length)
                currentFrame = 0;

            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}