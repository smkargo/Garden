using System.Collections;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [SerializeField] private GameObject butterflyPrefab;
    [SerializeField] private Sprite[] butterflySprites;

    [SerializeField] private float minSpawnTime = 15f;
    [SerializeField] private float maxSpawnTime = 30f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            SpawnButterfly();
        }
    }

    void SpawnButterfly()
    {
        bool moveRight = Random.value > 0.5f;

        float y = Random.Range(0.3f, 0.8f);

        Vector3 left =
            cam.ViewportToWorldPoint(new Vector3(-0.1f, y, 10));

        Vector3 right =
            cam.ViewportToWorldPoint(new Vector3(1.1f, y, 10));

        Vector3 spawnPos = moveRight ? left : right;

        GameObject butterfly =
            Instantiate(butterflyPrefab, spawnPos, Quaternion.identity);

        SpriteRenderer sr = butterfly.GetComponent<SpriteRenderer>();

        sr.sprite = butterflySprites[
            Random.Range(0, butterflySprites.Length)];

        butterfly.GetComponent<ButterflyMovement>()
                 .Initialize(moveRight);
    }
}