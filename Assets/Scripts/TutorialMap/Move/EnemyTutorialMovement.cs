using UnityEngine;

public class EnemyTutorialMovement : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float leftBoundary;
    public float rightBoundary;
    private EnemyTutorialStamina enemyStamina;
    private Vector3 targetPosition;  // Vị trí đích cần di chuyển tới
    private bool isMoving = false;
    private float moveSpeed = 2f;  // Tốc độ di chuyển mượt
    private Animator animator;
    private string triggerMoveLeft = "walk_left";
    private string triggerMoveRight = "walk_right";
    public AudioSource movementSound;
    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);
        enemyStamina = GetComponent<EnemyTutorialStamina>();
        animator = GetComponent<Animator>();

        // Giới hạn vị trí enemy ban đầu trong phạm vi map
        if (transform.position.x < leftBoundary)
            transform.position = new Vector3(leftBoundary + 0.5f, transform.position.y, transform.position.z);
        if (transform.position.x > rightBoundary)
            transform.position = new Vector3(rightBoundary - 0.5f, transform.position.y, transform.position.z);

        targetPosition = transform.position;  // Khởi tạo vị trí đích bằng vị trí ban đầu
    }

    void Update()
    {
        if (isMoving)
        {
            // Di chuyển mượt đến targetPosition
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Kiểm tra nếu đã tới vị trí đích
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void MoveLeft()
    {
        if (isMoving || enemyStamina.currentStamina <= 0) return;  // Đảm bảo không di chuyển khi đang di chuyển

        float newXPosition = transform.position.x - moveDistance;
        if (newXPosition >= leftBoundary)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            movementSound.Play();
            isMoving = true;  // Bắt đầu di chuyển
            animator.SetTrigger(triggerMoveLeft);  // Phát animation di chuyển trái
            enemyStamina.ReduceStamina(10f);
        }
    }

    public void MoveRight()
    {
        if (isMoving || enemyStamina.currentStamina <= 0) return;  // Đảm bảo không di chuyển khi đang di chuyển

        float newXPosition = transform.position.x + moveDistance;
        if (newXPosition <= rightBoundary)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            isMoving = true;  // Bắt đầu di chuyển
            movementSound.Play();
            animator.SetTrigger(triggerMoveRight);  // Phát animation di chuyển phải
            enemyStamina.ReduceStamina(10f);
        }
    }

    public void Rest()
    {
        if (enemyStamina.currentStamina < enemyStamina.maxStamina / 2)
        {
            enemyStamina.RegainStamina(20f);
        }
    }
}
