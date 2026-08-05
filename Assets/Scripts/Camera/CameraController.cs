using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Zoom")]
    [SerializeField] private float minSize = 4.5f;
    [SerializeField] private float maxSize = 8.5f;
    [SerializeField] private float padding = 1f;

    public void FitBoard(int width, int height)
    {
        float boardSize = Mathf.Max(width, height);

        float targetSize = boardSize * 0.55f + padding;

        cam.orthographicSize = Mathf.Clamp(
            targetSize,
            minSize,
            maxSize
        );
    }
}