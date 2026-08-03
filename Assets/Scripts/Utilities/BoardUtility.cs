using UnityEngine;

public static class BoardUtility
{
    public static int GetIndex(int x, int y, int width)
    {
        return y * width + x;
    }

    public static bool IsInsideBoard(int x, int y, int width, int height)
    {
        return x >= 0 &&
               y >= 0 &&
               x < width &&
               y < height;
    }

    public static Vector2Int[] GetCardinalDirections()
    {
        return new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };
    }
}