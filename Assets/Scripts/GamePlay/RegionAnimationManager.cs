using System.Collections;
using UnityEngine;

public class RegionAnimationManager : MonoBehaviour
{
    public static RegionAnimationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayCompleteAnimation(Region region)
    {
        StartCoroutine(BloomRoutine(region));
    }

    IEnumerator BloomRoutine(Region region)
    {
        foreach (Tile tile in region.Tiles)
        {
            tile.Complete(region.FlowerType);

            yield return new WaitForSeconds(0.04f);
        }
    }
}