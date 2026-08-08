using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform hand;
    [SerializeField] private TutorialManager tutorialManager;

    [Header("Animation")]
    [SerializeField] private float moveDurationPerTile = 0.35f;
    [SerializeField] private float pressDuration = 0.18f;

    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();

        if (hand == null)
            hand = GetComponent<RectTransform>();
    }
    public void ShowDemo(
        NumberTile numberTile,
        List<Tile> path)
    {
        if (numberTile == null)
            return;

        if (path == null || path.Count == 0)
            return;

        StopAllCoroutines();

        gameObject.SetActive(true);

        StartCoroutine(
            DemoRoutine(
                numberTile,
                path
            )
        );
    }

    private IEnumerator DemoRoutine(
        NumberTile numberTile,
        List<Tile> path)
    {
        Vector2 numberPosition =
            WorldToCanvasPosition(
                numberTile.transform.position
            );

        hand.anchoredPosition = numberPosition;

        yield return Click();

        yield return new WaitForSecondsRealtime(0.2f);

       
        foreach (Tile tile in path)
        {
            if (tile == null)
                continue;

            Vector2 targetPosition =
                WorldToCanvasPosition(
                    tile.transform.position
                );

            yield return MoveHand(
                hand.anchoredPosition,
                targetPosition,
                moveDurationPerTile
            );
        }

        yield return new WaitForSecondsRealtime(0.3f);

        FinishDemo();
    }
   public void ShowUndoDemo(NumberTile numberTile)
{
    if (numberTile == null)
        return;

    if (gameObject.activeSelf)
        return;

    gameObject.SetActive(true);

    StartCoroutine(
        UndoDemoRoutine(numberTile)
    );
}

    private IEnumerator UndoDemoRoutine(NumberTile numberTile)
{
    Vector2 numberPosition =
        WorldToCanvasPosition(
            numberTile.transform.position
        );

    hand.anchoredPosition = numberPosition;

    while (true)
    {
       
        yield return Click();
        yield return new WaitForSecondsRealtime(0.6f);

      
    }
}
    private void FinishDemo()
    {
        gameObject.SetActive(false);

        if (tutorialManager != null)
            tutorialManager.OnDemoFinished();
    }
    public void Hide()
    {
        StopAllCoroutines();

        if (hand != null)
            hand.localScale = Vector3.one;

        gameObject.SetActive(false);
    }

    
    public void StopDemo()
    {
        StopAllCoroutines();

        if (hand != null)
            hand.localScale = Vector3.one;

        gameObject.SetActive(false);
    }

   
    private IEnumerator MoveHand(
        Vector2 from,
        Vector2 to,
        float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;

            float t =
                Mathf.Clamp01(
                    time / duration
                );

            t =
                Mathf.SmoothStep(
                    0f,
                    1f,
                    t
                );

            hand.anchoredPosition =
                Vector2.Lerp(
                    from,
                    to,
                    t
                );

            yield return null;
        }

        hand.anchoredPosition = to;
    }

   
    private IEnumerator Click()
    {
        Vector3 originalScale =
            hand.localScale;

        hand.localScale =
            originalScale * 0.85f;

        yield return new WaitForSecondsRealtime(
            pressDuration
        );

        hand.localScale =
            originalScale;

        yield return new WaitForSecondsRealtime(
            0.12f
        );
    }
    private Vector2 WorldToCanvasPosition(
        Vector3 worldPosition)
    {
        Camera camera = Camera.main;

        Vector2 screenPosition =
            RectTransformUtility.WorldToScreenPoint(
                camera,
                worldPosition
            );

        RectTransform canvasRect =
            canvas.transform as RectTransform;

        RectTransformUtility
            .ScreenPointToLocalPointInRectangle(
                canvasRect,
                screenPosition,
                canvas.renderMode ==
                    RenderMode.ScreenSpaceOverlay
                    ? null
                    : canvas.worldCamera,
                out Vector2 localPosition
            );

        return localPosition;
    }
}