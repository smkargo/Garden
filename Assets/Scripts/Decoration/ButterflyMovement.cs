using System.Collections;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minSpeed = 1.5f;
    [SerializeField] private float maxSpeed = 2.5f;

    [Header("Natural Flight")]
    [SerializeField] private float directionChangeMin = 1.5f;
    [SerializeField] private float directionChangeMax = 3.5f;

    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float verticalMovement = 0.5f;

    [Header("Wing Animation")]
    [SerializeField] private float frameRate = 8f;

    private float speed;

    private Vector3 direction;
    private Vector3 targetDirection;

    private float directionTimer;
    private float nextDirectionChange;

    private Sprite[] animationFrames;
    private SpriteRenderer spriteRenderer;

    private int currentFrame;
    private float animationTimer;


    // =========================================================
    // INITIALIZE
    // =========================================================

    public void Initialize(
        bool moveRight,
        Sprite[] frames)
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();

        animationFrames = frames;

        speed =
            Random.Range(
                minSpeed,
                maxSpeed
            );

        // Initial direction
        direction =
            moveRight
                ? Vector3.right
                : Vector3.left;

        targetDirection =
            direction;

        // -----------------------------------------------------
        // ROTATE 90 DEGREES BASED ON FLIGHT DIRECTION
        // -----------------------------------------------------

        if (moveRight)
        {
            transform.rotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    -90f
                );
        }
        else
        {
            transform.rotation =
                Quaternion.Euler(
                    0f,
                    0f,
                    90f
                );
        }

        // Randomize first direction change
        nextDirectionChange =
            Random.Range(
                directionChangeMin,
                directionChangeMax
            );

        // Start animation
        currentFrame = 0;
        animationTimer = 0f;

        if (animationFrames != null &&
            animationFrames.Length > 0)
        {
            spriteRenderer.sprite =
                animationFrames[0];
        }
    }


    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        AnimateWings();

        UpdateFlightDirection();

        MoveButterfly();

        RotateToFlightDirection();

        CheckOutsideCamera();
    }


    // =========================================================
    // NATURAL DIRECTION CHANGES
    // =========================================================

    private void UpdateFlightDirection()
    {
        directionTimer +=
            Time.deltaTime;

        if (directionTimer >=
            nextDirectionChange)
        {
            directionTimer = 0f;

            nextDirectionChange =
                Random.Range(
                    directionChangeMin,
                    directionChangeMax
                );

            // Create a new natural direction.
            Vector2 randomDirection =
                Random.insideUnitCircle
                    .normalized;

            targetDirection =
                new Vector3(
                    randomDirection.x,
                    randomDirection.y,
                    0f
                );
        }

        // Smoothly turn toward target
        direction =
            Vector3.Lerp(
                direction,
                targetDirection,
                turnSpeed *
                Time.deltaTime
            ).normalized;
    }


    // =========================================================
    // MOVEMENT
    // =========================================================

    private void MoveButterfly()
    {
        transform.position +=
            direction *
            speed *
            Time.deltaTime;

        // Small additional flutter
        Vector3 position =
            transform.position;

        position.y +=
            Mathf.Sin(
                Time.time * 2.2f
            ) *
            verticalMovement *
            Time.deltaTime;

        transform.position =
            position;
    }


    // =========================================================
    // ROTATE TO FLIGHT DIRECTION
    // =========================================================

    private void RotateToFlightDirection()
    {
        if (direction.sqrMagnitude <
            0.001f)
            return;

        float angle =
            Mathf.Atan2(
                direction.y,
                direction.x
            ) *
            Mathf.Rad2Deg;

        Quaternion targetRotation =
            Quaternion.Euler(
                0f,
                0f,
                angle - 90f
            );

        transform.rotation =
            Quaternion.Lerp(
                transform.rotation,
                targetRotation,
                5f * Time.deltaTime
            );
    }


    // =========================================================
    // WING ANIMATION
    // =========================================================

    private void AnimateWings()
    {
        if (animationFrames == null ||
            animationFrames.Length == 0)
            return;

        animationTimer +=
            Time.deltaTime;

        float frameTime =
            1f / frameRate;

        if (animationTimer >= frameTime)
        {
            animationTimer -= frameTime;

            currentFrame++;

            if (currentFrame >=
                animationFrames.Length)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite =
                animationFrames[
                    currentFrame
                ];
        }
    }


    // =========================================================
    // OUTSIDE CAMERA
    // =========================================================

    private void CheckOutsideCamera()
    {
        Vector3 view =
            Camera.main.WorldToViewportPoint(
                transform.position
            );

        if (view.x < -0.2f ||
            view.x > 1.2f ||
            view.y < -0.2f ||
            view.y > 1.2f)
        {
            Destroy(gameObject);
        }
    }
}