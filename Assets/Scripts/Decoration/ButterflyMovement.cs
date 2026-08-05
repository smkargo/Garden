using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1.5f;
    [SerializeField] private float maxSpeed = 2.5f;

    [SerializeField] private float minWave = 0.2f;
    [SerializeField] private float maxWave = 0.5f;

    [SerializeField] private float minWaveSpeed = 1f;
    [SerializeField] private float maxWaveSpeed = 2f;

    private float speed;
    private float waveHeight;
    private float waveSpeed;

    private Vector3 direction;
    private float timer;
    private float startY;

   public void Initialize(bool moveRight)
{
    direction = moveRight ? Vector3.right : Vector3.left;

    speed = Random.Range(minSpeed, maxSpeed);
    waveHeight = Random.Range(minWave, maxWave);
    waveSpeed = Random.Range(minWaveSpeed, maxWaveSpeed);

    startY = transform.position.y;

    // Rotate the butterfly so it faces the flying direction
    if (moveRight)
        transform.rotation = Quaternion.Euler(0, 0, -90);
    else
        transform.rotation = Quaternion.Euler(0, 0, 90);
}
    void Update()
    {
        timer += Time.deltaTime;

        transform.position += direction * speed * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y = startY + Mathf.Sin(timer * waveSpeed) * waveHeight;
        transform.position = pos;

        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);

        if (view.x < -0.2f || view.x > 1.2f)
            Destroy(gameObject);
    }
}