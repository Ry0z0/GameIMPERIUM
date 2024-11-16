using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveSpeed = 2f;
    public float leftBoundary;
    public float rightBoundary;

    private Vector3 targetPosition;
    public bool isMoving = false;
    private Animator animator;
    private string triggerMoveLeft = "walk_left";
    private string triggerMoveRight = "walk_right";
    public AudioSource movementSound;
    private EnemyNormalManagement enemyNormalManagement;

    public void Start()
    {
        enemyNormalManagement = GetComponent<EnemyNormalManagement>();
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);
        animator = GetComponent<Animator>();
        if (transform.position.x < leftBoundary)
            transform.position = new Vector3(leftBoundary + 0.5f, transform.position.y, transform.position.z);
        if (transform.position.x > rightBoundary)
            transform.position = new Vector3(rightBoundary - 0.5f, transform.position.y, transform.position.z);
    }
    public void MoveLeft()
    {
        if (isMoving || enemyNormalManagement.stats.currentStamina <= 0) return;

        float newXPosition = transform.position.x - moveDistance;
        if (newXPosition >= leftBoundary)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            isMoving = true;
            movementSound.Play();
            animator.SetTrigger(triggerMoveLeft);
            enemyNormalManagement.ReduceStamina(10);
        }
    }

    public void MoveRight()
    {
        if (isMoving || enemyNormalManagement.stats.currentStamina <= 0) return;

        float newXPosition = transform.position.x + moveDistance;
        if (newXPosition <= rightBoundary)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            isMoving = true;
            movementSound.Play();
            animator.SetTrigger(triggerMoveRight);
            enemyNormalManagement.ReduceStamina(10);
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
