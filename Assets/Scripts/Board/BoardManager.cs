using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private NumberTile numberTilePrefab;

    [Header("Current Level")]
    [SerializeField] private LevelData currentLevel;
}