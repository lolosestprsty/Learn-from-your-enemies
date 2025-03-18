// TeacherAI.cs
using System.Collections;
using UnityEngine;

public class TeacherAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] patrolPoints; // Left and right patrol points
    public float moveSpeed = 2f;
    public float waitTimeAtPoints = 1f;

    [Header("Visual Settings")]
    public Sprite facingPlayerSprite; // Red square (e.g.)
    public Sprite notFacingPlayerSprite; // Green square (e.g.)

    private int currentTargetIndex = 0;
    private bool isMoving = true;
    private SpriteRenderer spriteRenderer;

    public bool IsFacingPlayer { get; private set; }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PatrolRoutine());
    }

    IEnumerator PatrolRoutine()
    {
        while (true)
        {
            // Move towards current target point
            while (Vector3.Distance(transform.position, patrolPoints[currentTargetIndex].position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    patrolPoints[currentTargetIndex].position,
                    moveSpeed * Time.deltaTime
                );

                UpdateFacingDirection();
                yield return null;
            }

            // Wait at point
            isMoving = false;
            yield return new WaitForSeconds(waitTimeAtPoints);
            
            // Switch target point
            currentTargetIndex = (currentTargetIndex + 1) % patrolPoints.Length;
            isMoving = true;
        }
    }

    void UpdateFacingDirection()
    {
        // Determine if moving left or right
        bool movingLeft = patrolPoints[currentTargetIndex].position.x < transform.position.x;

        // Update sprite and facing state
        if (movingLeft)
        {
            spriteRenderer.sprite = facingPlayerSprite;
            IsFacingPlayer = true;
        }
        else
        {
            spriteRenderer.sprite = notFacingPlayerSprite;
            IsFacingPlayer = false;
        }
    }
}