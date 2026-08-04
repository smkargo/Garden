using TMPro;
using UnityEngine;

public class NumberTile : Tile
{
    [SerializeField] private TMP_Text numberText;

    public int TargetSize { get; private set; }

    public void SetRegionSize(int size)
    {
        TargetSize = size;

        if (numberText != null)
            numberText.text = size.ToString();
    }
    public override void Initialize(Vector2Int position)
{
    base.Initialize(position);
}
}