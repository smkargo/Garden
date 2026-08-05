using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FullscreenBackground : MonoBehaviour
{
    Camera cam;
    SpriteRenderer sr;

    float lastSize;
    float lastAspect;

    void Awake()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (cam == null)
            return;

        if (lastSize != cam.orthographicSize ||
            lastAspect != cam.aspect)
        {
            FitToCamera();

            lastSize = cam.orthographicSize;
            lastAspect = cam.aspect;
        }
    }

    public void FitToCamera()
    {
        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;

        transform.localScale = new Vector3(
            worldWidth / spriteSize.x,
            worldHeight / spriteSize.y,
            1f);
    }
}