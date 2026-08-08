using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private LevelDatabase database;
    [SerializeField] private LevelButtonUI levelButtonPrefab;

    [Header("Page")]
    [SerializeField] private RectTransform viewport;
    [SerializeField] private RectTransform pages;

    [Header("Grid")]
    [SerializeField] private int columns = 5;
    [SerializeField] private int rows = 2;

    [Header("Navigation")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [Header("Slide")]
    [SerializeField] private float slideDuration = 0.35f;

    private readonly List<RectTransform> pageList = new();

    private int currentPage = 0;
    private int totalPages = 0;

    private Coroutine slideCoroutine;

    private void Start()
    {
        GeneratePages();

        // Make sure listeners are not duplicated.
        leftButton.onClick.RemoveListener(PreviousPage);
        rightButton.onClick.RemoveListener(NextPage);

        leftButton.onClick.AddListener(PreviousPage);
        rightButton.onClick.AddListener(NextPage);

        UpdateNavigation();
    }

    private void GeneratePages()
    {
        ClearPages();

        int levelsPerPage = columns * rows;

        if (database == null)
        {
           
            return;
        }

        if (viewport == null || pages == null)
        {
           
            return;
        }

        if (levelButtonPrefab == null)
        {
            
            return;
        }

        totalPages = Mathf.CeilToInt(
            (float)database.Levels.Count / levelsPerPage
        );

        currentPage = 0;

        for (int pageIndex = 0; pageIndex < totalPages; pageIndex++)
        {
            CreatePage(pageIndex, levelsPerPage);
        }

        UpdatePagesPosition();
        UpdateNavigation();
    }

    private void CreatePage(
        int pageIndex,
        int levelsPerPage)
    {
        GameObject pageObject = new GameObject(
            "Page_" + (pageIndex + 1),
            typeof(RectTransform)
        );

        RectTransform page =
            pageObject.GetComponent<RectTransform>();

        page.SetParent(pages, false);

        page.anchorMin = new Vector2(0, 0);
        page.anchorMax = new Vector2(0, 1);
        page.pivot = new Vector2(0, 0.5f);

        page.sizeDelta = new Vector2(
            viewport.rect.width,
            0
        );

        page.anchoredPosition = new Vector2(
            pageIndex * viewport.rect.width,
            0
        );

        GridLayoutGroup grid =
            pageObject.AddComponent<GridLayoutGroup>();

        grid.constraint =
            GridLayoutGroup.Constraint.FixedColumnCount;

        grid.constraintCount = columns;

        grid.cellSize =
            new Vector2(200f, 200f);

        grid.spacing =
            new Vector2(30f, 30f);

        grid.startCorner =
            GridLayoutGroup.Corner.UpperLeft;

        grid.startAxis =
            GridLayoutGroup.Axis.Horizontal;

        grid.childAlignment =
            TextAnchor.MiddleCenter;

        int startIndex =
            pageIndex * levelsPerPage;

        int endIndex =
            Mathf.Min(
                startIndex + levelsPerPage,
                database.Levels.Count
            );

        for (int i = startIndex; i < endIndex; i++)
        {
            LevelButtonUI button =
                Instantiate(
                    levelButtonPrefab,
                    page
                );

            button.Setup(
                i,
                SaveManager.Instance.IsUnlocked(i),
                SaveManager.Instance.GetStars(i)
            );
        }

        pageList.Add(page);
    }

    private void ClearPages()
    {
        pageList.Clear();

        for (int i = pages.childCount - 1; i >= 0; i--)
        {
            Destroy(
                pages.GetChild(i).gameObject
            );
        }
    }

    public void NextPage()
    {
        if (totalPages <= 1)
            return;

        if (currentPage >= totalPages - 1)
            return;

        currentPage++;

        SlideToCurrentPage();
    }
    public void PreviousPage()
    {
        if (totalPages <= 1)
            return;

        if (currentPage <= 0)
            return;

        currentPage--;

        SlideToCurrentPage();
    }
    private void SlideToCurrentPage()
    {
        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
        }

        slideCoroutine =
            StartCoroutine(
                SlideToPage(currentPage)
            );
    }

    private IEnumerator SlideToPage(
        int pageIndex)
    {
        Vector2 startPosition =
            pages.anchoredPosition;

        Vector2 targetPosition =
            new Vector2(
                -pageIndex *
                viewport.rect.width,
                0
            );

        float time = 0f;

        while (time < slideDuration)
        {
            time += Time.unscaledDeltaTime;

            float t =
                Mathf.Clamp01(
                    time / slideDuration
                );

            t =
                Mathf.SmoothStep(
                    0f,
                    1f,
                    t
                );

            pages.anchoredPosition =
                Vector2.Lerp(
                    startPosition,
                    targetPosition,
                    t
                );

            yield return null;
        }

        pages.anchoredPosition =
            targetPosition;

        slideCoroutine = null;

        UpdateNavigation();
    }
    private void UpdatePagesPosition()
    {
        pages.anchoredPosition =
            Vector2.zero;
    }
    private void UpdateNavigation()
    {
        if (leftButton == null ||
            rightButton == null)
            return;

        leftButton.interactable =
            currentPage > 0;

        rightButton.interactable =
            currentPage < totalPages - 1;

    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
    }
   }