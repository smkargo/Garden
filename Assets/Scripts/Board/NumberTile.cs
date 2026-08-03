using TMPro;
using UnityEngine;

public class NumberTile : Tile
{
    [SerializeField] private TMP_Text numberText;

    public void SetRegionSize(int size)
    {
        numberText.text = size.ToString();
    }
}