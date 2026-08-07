using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private LevelButtonUI levelButtonPrefab;
    [SerializeField] private LevelDatabase database;

    private void Start()
    {
        GenerateButtons();
    }

    private void GenerateButtons()
{
    for (int i = 0; i < database.Levels.Count; i++)
    {
        LevelButtonUI button =
            Instantiate(levelButtonPrefab, content);

        button.Setup(
            i,
            SaveManager.Instance.IsUnlocked(i),
            SaveManager.Instance.GetStars(i));
    }
}
}