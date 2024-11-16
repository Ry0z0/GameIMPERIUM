using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementV2 : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float leftBoundary;
    public float rightBoundary;
    private Animator animator;
    private string triggerWalkLeft = "walk_left";
    private string triggerWalkRight = "walk_right";
    private PlayerStamina playerStamina;
    private float moveSpeed = 2f;  // Tốc độ di chuyển mượt
    private Vector3 targetPosition;
    private bool isMoving = false;
    public GameObject player;  // Đối tượng nhân vật
    public Transform enemyTransform;
    private Rigidbody2D rb;
    public float distance;
    public AudioSource movementSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 1f;  // Điều chỉnh trọng lực, 1 là mặc định
        rb.mass = 1f;  // Điều chỉnh khối lượng của nhân vật (nếu cần)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        CapsuleCollider2D capsuleCollider = gameObject.AddComponent<CapsuleCollider2D>();
        capsuleCollider.size = new Vector2(1f, 1f);

        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);

        if (player.transform.position.x < leftBoundary)
            player.transform.position = new Vector3(leftBoundary + 0.5f, player.transform.position.y, player.transform.position.z);
        if (player.transform.position.x > rightBoundary)
            player.transform.position = new Vector3(rightBoundary - 0.5f, player.transform.position.y, player.transform.position.z);

        animator = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
        targetPosition = transform.position;  // Bắt đầu từ vị trí hiện tại
    }

    void Update()
    {
        if (isMoving)
        {
            // Di chuyển mượt dần dần đến targetPosition
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Kiểm tra nếu đã tới vị trí đích
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
        distance = Vector3.Distance(transform.position, enemyTransform.position);
    }

    public void MoveForward()
    {
        if (isMoving || playerStamina.currentStamina <= 0) return;  // Đảm bảo không di chuyển khi đang di chuyển

        float newXPosition = transform.position.x + moveDistance;
        if (newXPosition <= rightBoundary)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            isMoving = true;  // Bắt đầu di chuyển
            movementSound.Play();
            animator.SetTrigger(triggerWalkRight);  // Phát animation ngay lập tức
            playerStamina.ReduceStamina(10f);
        }
    }

    public void MoveBackward()
    {
        if (isMoving || playerStamina.currentStamina <= 0) return;  // Đảm bảo không di chuyển khi đang di chuyển

        float newXPosition = transform.position.x - moveDistance;
        if (newXPosition >= leftBoundary)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            isMoving = true;  // Bắt đầu di chuyển'
            movementSound.Play();
            animator.SetTrigger(triggerWalkLeft);  // Phát animation ngay lập tức
            playerStamina.ReduceStamina(10f);
        }
    }
}
