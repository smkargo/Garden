using System.Collections;
using UnityEngine;
using TMPro;

public class LevelCompleteUI : MonoBehaviour
{
    [SerializeField] private Animator panelAnimator;
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator[] starAnimators;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text moveText;

    [Header("Animation Timing")]
    [SerializeField] private float panelDelay = 0.4f;
    [SerializeField] private float panelAnimationDuration = 0.5f;
    [SerializeField] private float starDelay = 0.25f;

    private Coroutine showCoroutine;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);

        HideStars();
    }

    public void Show()
    {
        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        showCoroutine = StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
            showCoroutine = null;
        }

        if (panel != null)
            panel.SetActive(false);

        HideStars();
    }

    private IEnumerator ShowRoutine()
    {
        // -------------------------------------------------
        // WAIT BEFORE SHOWING COMPLETION PANEL
        // -------------------------------------------------

        yield return new WaitForSecondsRealtime(panelDelay);

        // -------------------------------------------------
        // SHOW PANEL
        // -------------------------------------------------

        panel.SetActive(true);

        // Reset panel animation
        panelAnimator.Rebind();
        panelAnimator.Update(0f);

        // Play panel entrance
        panelAnimator.Play("PanelShow", 0, 0f);

        // Level complete sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayLevelComplete();
        }

        // -------------------------------------------------
        // SET TEXT
        // -------------------------------------------------

        int stars =
            MoveManager.Instance.GetStars();

        switch (stars)
        {
            case 3:
                resultText.text = "Perfect Garden!";
                break;

            case 2:
                resultText.text = "Beautiful Garden!";
                break;

            case 1:
                resultText.text = "Garden Restored!";
                break;

            default:
                resultText.text = "Keep Growing!";
                break;
        }

        // -------------------------------------------------
        // MOVE TEXT
        // -------------------------------------------------

        if (stars == 3)
        {
            moveText.text =
                $"Completed in {MoveManager.Instance.CurrentMoves} moves";
        }
        else
        {
            moveText.text =
                $"Completed in {MoveManager.Instance.CurrentMoves} moves\n" +
                $"Perfect: {MoveManager.Instance.PerfectMoves} moves";
        }

        // -------------------------------------------------
        // WAIT FOR PANEL ANIMATION
        // -------------------------------------------------

        yield return new WaitForSecondsRealtime(
            panelAnimationDuration
        );

        // -------------------------------------------------
        // NOW SHOW STARS
        // -------------------------------------------------

        yield return StartCoroutine(
            ShowStars(stars)
        );

        showCoroutine = null;
    }

   private IEnumerator ShowStars(int stars)
{
    for (int i = 0; i < stars; i++)
    {
        Animator star = starAnimators[i];

        if (star == null)
            continue;

        // Make sure the star is active.
        star.gameObject.SetActive(true);

        // Reset animation while the panel AND star are active.
        star.Rebind();
        star.Update(0f);

        // Play popup animation.
        star.Play("starpopup", 0, 0f);

        // Star reveal sound.
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayStarReveal();
        }

        yield return new WaitForSecondsRealtime(starDelay);
    }
}
   private void HideStars()
{
    foreach (Animator star in starAnimators)
    {
        if (star == null)
            continue;

        star.gameObject.SetActive(false);
    }
}
}