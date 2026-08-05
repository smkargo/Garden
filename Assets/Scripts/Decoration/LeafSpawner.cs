using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    [SerializeField] private GameObject leafPrefab;
    [SerializeField] private Sprite[] leafSprites;

    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 3f;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        Invoke(nameof(SpawnLeaf), 1f);
    }

    void SpawnLeaf()
    {
        Vector3 left =
            cam.ViewportToWorldPoint(new Vector3(0, 1.1f, 10));

        Vector3 right =
            cam.ViewportToWorldPoint(new Vector3(1, 1.1f, 10));

        float x = Random.Range(left.x - 2f, right.x + 2f);

        Vector3 spawnPos = new Vector3(x, left.y, 0);

        GameObject leaf = Instantiate(leafPrefab, spawnPos, Quaternion.identity);

        SpriteRenderer sr = leaf.GetComponent<SpriteRenderer>();

        sr.sprite = leafSprites[Random.Range(0, leafSprites.Length)];

        Invoke(nameof(SpawnLeaf),
            Random.Range(minSpawnTime, maxSpawnTime));
    }
}