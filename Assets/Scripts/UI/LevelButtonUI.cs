using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject[] stars;

    private int levelIndex;

    public void Setup(int index, bool unlocked, int starCount)
    {
        levelIndex = index;

        levelText.text = (index + 1).ToString();
        levelText.gameObject.SetActive(unlocked);
        lockIcon.SetActive(!unlocked);

        button.interactable = unlocked;

        for (int i = 0; i < stars.Length; i++)
{
    stars[i].SetActive(unlocked && i < starCount);
}

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        GameSession.SelectedLevel = levelIndex;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}