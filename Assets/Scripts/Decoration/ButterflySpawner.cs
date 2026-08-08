using System.Collections;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject butterflyPrefab;

    [Header("Butterfly Frames")]
    [SerializeField] private Sprite[] butterflySprites;

    [Header("Spawn Time")]
    [SerializeField] private float minSpawnTime = 15f;
    [SerializeField] private float maxSpawnTime = 30f;

    private Camera cam;


    private void Start()
    {
        cam = Camera.main;

        StartCoroutine(SpawnRoutine());
    }


    // =========================================================
    // SPAWN ROUTINE
    // =========================================================

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(
                    minSpawnTime,
                    maxSpawnTime
                )
            );

            SpawnButterfly();
        }
    }


    // =========================================================
    // SPAWN BUTTERFLY
    // =========================================================

    private void SpawnButterfly()
    {
        bool moveRight =
            Random.value > 0.5f;

        float y =
            Random.Range(
                0.3f,
                0.8f
            );

        Vector3 left =
            cam.ViewportToWorldPoint(
                new Vector3(
                    -0.1f,
                    y,
                    10
                )
            );

        Vector3 right =
            cam.ViewportToWorldPoint(
                new Vector3(
                    1.1f,
                    y,
                    10
                )
            );

        Vector3 spawnPos =
            moveRight
                ? left
                : right;


        GameObject butterfly =
            Instantiate(
                butterflyPrefab,
                spawnPos,
                Quaternion.identity
            );


        // =====================================================
        // SELECT BUTTERFLY TYPE
        // =====================================================

        int butterflyType =
            Random.Range(0, 3);


        // Each butterfly has 4 animation frames.
        int startIndex =
            butterflyType * 4;


        Sprite[] frames =
            new Sprite[4];


        for (int i = 0; i < 4; i++)
        {
            frames[i] =
                butterflySprites[
                    startIndex + i
                ];
        }


        // =====================================================
        // INITIALIZE
        // =====================================================

        butterfly
            .GetComponent<ButterflyMovement>()
            .Initialize(
                moveRight,
                frames
            );
    }
}