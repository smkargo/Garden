using UnityEngine;

public class NumberTile : Tile
{
    [Header("Region")]
    public int RegionSize;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer numberRenderer;
}