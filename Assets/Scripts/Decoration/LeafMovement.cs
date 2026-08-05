using UnityEngine;

public class LeafMovement : MonoBehaviour
{
    public float fallSpeed = 1.5f;
    public float swaySpeed = 2f;
    public float swayAmount = 0.4f;
    public float rotationSpeed = 40f;

    private float startX;
    private float timer;

   void Start()
{
    startX = transform.position.x;
    timer = Random.Range(0f, 10f);

    // Random size
    float scale = Random.Range(0.6f, 0.8f);
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    transform.localScale = Vector3.one * scale;

    // Random falling speed
    fallSpeed = Random.Range(0.8f, 2f);

    // Random sway
    swayAmount = Random.Range(0.2f, 0.6f);
    swaySpeed = Random.Range(1f, 3f);

    // Random rotation direction
    rotationSpeed = Random.Range(-60f, 60f);
}

    void Update()
    {
        timer += Time.deltaTime;

        Vector3 pos = transform.position;

        pos.y -= fallSpeed * Time.deltaTime;
        pos.x = startX + Mathf.Sin(timer * swaySpeed) * swayAmount;

        transform.position = pos;

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        if (Camera.main.WorldToViewportPoint(transform.position).y < -0.2f)
        {
            Destroy(gameObject);
        }
    }
}