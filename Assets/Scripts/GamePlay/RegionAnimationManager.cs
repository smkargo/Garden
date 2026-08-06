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

   private IEnumerator BloomRoutine(Region region)
{
    AudioManager.Instance.PlayFlower();

    foreach (Tile tile in region.Tiles)
    {
        tile.Complete(region.FlowerType);

        yield return new WaitForSeconds(0.08f);
    }

    // Wait for the last bloom animation to finish
    yield return new WaitForSeconds(0.5f);

    GameManager.Instance.CheckWin();
}
}