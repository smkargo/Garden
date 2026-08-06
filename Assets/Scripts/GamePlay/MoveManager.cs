using UnityEngine;
using TMPro;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance;

    public int PerfectMoves { get; private set; }
    public int CurrentMoves { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize(int perfectMoves)
{
    PerfectMoves = perfectMoves;
    CurrentMoves = 0;
}

    public void AddMove()
    {
        CurrentMoves++;
    }
    public int GetStars()
{
    if (CurrentMoves <= PerfectMoves)
        return 3;

    float ratio = (float)CurrentMoves / PerfectMoves;

    if (ratio <= 1.2f)
        return 2;

    if (ratio <= 1.5f)
        return 1;

    return 0;
}


}