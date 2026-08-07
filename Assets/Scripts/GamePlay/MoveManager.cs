using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance;

    public int PerfectMoves { get; private set; }
    public int CurrentMoves { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize(IReadOnlyList<Region> regions)
    {
        CurrentMoves = 0;
        PerfectMoves = CalculatePerfectMoves(regions);
    }

    private int CalculatePerfectMoves(IReadOnlyList<Region> regions)
    {
        int moves = 0;

        foreach (Region region in regions)
        {
            moves += region.TargetSize - 1;
        }

        return moves;
    }

    public void AddMove()
    {
        CurrentMoves++;
    }

    public void ResetMoves()
    {
        CurrentMoves = 0;
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