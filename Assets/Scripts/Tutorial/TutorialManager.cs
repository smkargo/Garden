using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TutorialHand tutorialHand;
    [Header("UI")]
    [SerializeField] private GameObject skipButton;
    [Header("Tutorial")]
    [SerializeField] private bool enableTutorial = true;
    [SerializeField] private float repeatDelay = 0.6f;

    private enum TutorialStep
    {
        None,
        TeachingDrag,
        PlayerDragging,
        TeachingUndo,
        PlayerUndo,
        Complete
    }

    private TutorialStep step = TutorialStep.None;

    private NumberTile tutorialNumberTile;
    private readonly List<Tile> tutorialPath = new();

    private int currentPathIndex = -1;

    private Coroutine tutorialLoop;
    private bool playerHasTakenControl = false;

    private void Start()
{
    if (skipButton != null)
    {
        skipButton.SetActive(
            enableTutorial &&
            GameSession.SelectedLevel == 0
        );
    }

    // Tutorial disabled.
    if (!enableTutorial)
        return;

    // Tutorial only runs on Level 1.
    if (GameSession.SelectedLevel != 0)
        return;

    Invoke(nameof(StartTutorial), 0.5f);
}

    private void StartTutorial()
    {
        if (boardManager == null || tutorialHand == null)
            return;

        tutorialNumberTile = FindFirstNumberTile();

        if (tutorialNumberTile == null)
        {
            return;
        }

        BuildTutorialPath();

        if (tutorialPath.Count != 4)
        {
            return;
        }

        StartDragTeaching();
    }

    private void StartDragTeaching()
    {
        StopTutorial();

        playerHasTakenControl = false;
        currentPathIndex = -1;

        step = TutorialStep.TeachingDrag;

        PlayDragDemo();
    }

private void PlayDragDemo()
{
    if (step != TutorialStep.TeachingDrag)
        return;

    if (playerHasTakenControl)
        return;

    tutorialHand.ShowDemo(
        tutorialNumberTile,
        tutorialPath
    );
}


    public void OnNumberTilePressed(NumberTile numberTile)
    {
        if (numberTile == null)
            return;

        if (GameSession.SelectedLevel != 0)
            return;

        if (numberTile != tutorialNumberTile)
            return;

        if (step == TutorialStep.TeachingDrag)
        {
            
            playerHasTakenControl = true;

            // Change state immediately.
            step = TutorialStep.PlayerDragging;

            // Stop the tutorial loop.
            StopTutorial();

            // Hide hand immediately.
            if (tutorialHand != null)
                tutorialHand.StopDemo();

            currentPathIndex = -1;

            return;
        }

        if (step == TutorialStep.TeachingUndo)
        {
            
            playerHasTakenControl = true;

            step = TutorialStep.PlayerUndo;

            StopTutorial();

            if (tutorialHand != null)
                tutorialHand.StopDemo();

            return;
        }
    }
    public void OnDemoFinished()
{
    if (playerHasTakenControl)
        return;

    if (step == TutorialStep.TeachingDrag)
    {
        tutorialLoop =
            StartCoroutine(RepeatDragDemo());

        return;
    }

    if (step == TutorialStep.TeachingUndo)
    {
        tutorialLoop =
            StartCoroutine(RepeatUndoDemo());
    }
}
private IEnumerator RepeatDragDemo()
{
    yield return new WaitForSecondsRealtime(
        repeatDelay
    );

    tutorialLoop = null;

    if (step != TutorialStep.TeachingDrag)
        yield break;

    if (playerHasTakenControl)
        yield break;

    PlayDragDemo();
}

private IEnumerator RepeatUndoDemo()
{
    yield return new WaitForSecondsRealtime(
        repeatDelay
    );

    tutorialLoop = null;

    if (step != TutorialStep.TeachingUndo)
        yield break;

    if (playerHasTakenControl)
        yield break;

    tutorialHand.ShowUndoDemo(
        tutorialNumberTile
    );
}

      public void OnTutorialTileEntered(Tile tile)
{
    if (step != TutorialStep.PlayerDragging)
        return;

    if (tile == null)
        return;

    int index = tutorialPath.IndexOf(tile);

   
    if (index == -1)
        return;

    if (index <= currentPathIndex)
        return;

    int expectedIndex = currentPathIndex + 1;

    
    if (index != expectedIndex)
    {
        return;
    }

    currentPathIndex = index;

    if (currentPathIndex >= tutorialPath.Count - 1)
    {
        DragCompleted();
    }
}

    public void OnPlayerReleased()
    {
        if (step != TutorialStep.PlayerDragging)
            return;

        if (currentPathIndex <
            tutorialPath.Count - 1)
        {
            RestartDragTeaching();
        }
    }

    private void DragCompleted()
    {
        StopTutorial();

        if (tutorialHand != null)
            tutorialHand.StopDemo();

        currentPathIndex = -1;

        // Reset control flag for the NEXT tutorial phase.
        playerHasTakenControl = false;

        // Move to undo teaching.
        step = TutorialStep.TeachingUndo;

        tutorialLoop =
            StartCoroutine(
                StartUndoTeachingRoutine()
            );
    }private IEnumerator StartUndoTeachingRoutine()
{
    yield return new WaitForSecondsRealtime(
        repeatDelay
    );

    tutorialLoop = null;

    if (step != TutorialStep.TeachingUndo)
        yield break;

    if (playerHasTakenControl)
        yield break;

    tutorialHand.ShowUndoDemo(
        tutorialNumberTile
    );
}

    private void RestartDragTeaching()
{
    StopTutorial();

    if (tutorialHand != null)
        tutorialHand.StopDemo();

    playerHasTakenControl = false;
    currentPathIndex = -1;

    step = TutorialStep.TeachingDrag;

    PlayDragDemo();
}

    public void OnUndoSuccessful()
    {
        if (step != TutorialStep.PlayerUndo)
            return;

        CompleteTutorial();
    }
    private void CompleteTutorial()
{
    StopTutorial();

    if (tutorialHand != null)
        tutorialHand.StopDemo();

    playerHasTakenControl = true;

    step = TutorialStep.Complete;

    if (skipButton != null)
        skipButton.SetActive(false);
}
    private void StopTutorial()
    {
        if (tutorialLoop != null)
        {
            StopCoroutine(tutorialLoop);
            tutorialLoop = null;
        }

        if (tutorialHand != null)
            tutorialHand.StopDemo();
    }
 private void BuildTutorialPath()
    {
        tutorialPath.Clear();

        Vector2Int start =
            tutorialNumberTile.GridPosition;

        for (int i = 1; i <= 4; i++)
        {
            Vector2Int position =
                start +
                Vector2Int.right * i;

            BoardCell cell =
                boardManager.GetCell(position);

            if (cell == null)
            {
                tutorialPath.Clear();
                return;
            }

            tutorialPath.Add(cell.Tile);
        }
    }
    private NumberTile FindFirstNumberTile()
    {
        for (
            int y = 0;
            y < boardManager.Level.Board.Height;
            y++)
        {
            for (
                int x = 0;
                x < boardManager.Level.Board.Width;
                x++)
            {
                BoardCell cell =
                    boardManager.GetCell(
                        new Vector2Int(x, y)
                    );

                if (cell == null)
                    continue;

                if (cell.Tile is NumberTile numberTile)
                    return numberTile;
            }
        }

        return null;
    }
   public void SkipTutorial()
{
    StopTutorial();

    if (tutorialHand != null)
        tutorialHand.StopDemo();

    playerHasTakenControl = true;

    step = TutorialStep.Complete;

    if (skipButton != null)
        skipButton.SetActive(false);
}
public void OnLevelChanged()
{
    StopTutorial();

    playerHasTakenControl = false;
    currentPathIndex = -1;
    if (GameSession.SelectedLevel == 0)
    {
        if (skipButton != null)
            skipButton.SetActive(enableTutorial);

        step = TutorialStep.None;

        Invoke(nameof(StartTutorial), 0.5f);
    }
    else
    {
        // Levels 2+
        if (skipButton != null)
            skipButton.SetActive(false);

        step = TutorialStep.Complete;
    }
}
}