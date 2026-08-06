using System.Collections;
using UnityEngine;
using TMPro;

public class LevelCompleteUI : MonoBehaviour
{
    public static LevelCompleteUI Instance;

    [SerializeField] private Animator panelAnimator;
    [SerializeField] private GameObject panel;
    [SerializeField] private Animator[] starAnimators;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text moveText;

    private void Awake()
    {
        Instance = this;

        panel.SetActive(false);

        foreach (Animator star in starAnimators)
            star.gameObject.SetActive(false);
    }

    public void Show()
    {
        StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

   private IEnumerator ShowRoutine()
{
    panel.SetActive(true);

    panelAnimator.Play("PanelShow", 0, 0f);

    yield return new WaitForSeconds(0.4f);

    int stars = MoveManager.Instance.GetStars();

    // Set the result message
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

    // Set the move information
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

    yield return StartCoroutine(ShowStars(stars));
}

    private IEnumerator ShowStars(int stars)
    {
        for (int i = 0; i < stars; i++)
        {
            starAnimators[i].gameObject.SetActive(true);

            starAnimators[i].Play("StarPop", 0, 0f);

            yield return new WaitForSeconds(0.25f);
        }
    }
}