using UnityEngine;
using UnityEngine.UI;

public class UIButtonManagement : MonoBehaviour
{
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public Button weakAttackButton;
    public Button normalAttackButton;
    public Button strongAttackButton;
    public Button restButton;
    private TurnNormalManager turnManager;
    private PlayerStamina playerStamina;
    private PlayerMovementV2 playerMovement;
    private PlayerAttack playerAttack;
    private Transform playerTransform;
    private Transform enemyTransform;
    private EnemyMovement enemyMovement;
    private BossMovement bossMovement;
    public float leftBoundary;
    public float rightBoundary;
    public float Vitrikedich = 1f;
    public float Vitrichemgan = 2f;


    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);

        turnManager = FindObjectOfType<TurnNormalManager>();
        playerMovement = FindObjectOfType<PlayerMovementV2>();
        playerStamina = playerMovement.GetComponent<PlayerStamina>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerTransform = playerMovement.transform;
        enemyMovement = FindObjectOfType<EnemyMovement>();
        bossMovement = FindObjectOfType<BossMovement>();
        enemyTransform = GameObject.FindWithTag("enemy").transform;

        // Thêm các listener cho các nút
        moveForwardButton.onClick.AddListener(() => {
            playerMovement.MoveForward();
            turnManager.IsPlayerTurn = false;
            turnManager.EndPlayerTurn();
        });

        moveBackwardButton.onClick.AddListener(() => {
            playerMovement.MoveBackward();
            turnManager.IsPlayerTurn = false;
            turnManager.EndPlayerTurn();
        });

        weakAttackButton.onClick.AddListener(() => {
            playerAttack.AttackWeak();
            turnManager.IsPlayerTurn = false;
            turnManager.EndPlayerTurn();
        });

        normalAttackButton.onClick.AddListener(() => {
            playerAttack.AttackNormal();
            turnManager.IsPlayerTurn = false;
            turnManager.EndPlayerTurn();
        });

        strongAttackButton.onClick.AddListener(() => {
            playerAttack.AttackStrong();
            turnManager.IsPlayerTurn = false;
            turnManager.EndPlayerTurn();
        });

        restButton.onClick.AddListener(() => {
            playerStamina.RegainStamina(20f);
            turnManager.IsPlayerTurn = false;
            turnManager.EndPlayerTurn();
        });
    }

    void Update()
    {
        bool isPlayerTurn = turnManager.IsPlayerTurn;
        float distanceToEnemy;
        if (this.gameObject.scene.name == "Boss1")
        {
            distanceToEnemy = Vector3.Distance(playerTransform.position, bossMovement.transform.position);

        }
        else if (this.gameObject.scene.name == "Boss2")
        {
            distanceToEnemy = Vector3.Distance(playerTransform.position, bossMovement.transform.position);

        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            distanceToEnemy = Vector3.Distance(playerTransform.position, bossMovement.transform.position);

        }
        else
        {
            distanceToEnemy = Vector3.Distance(playerTransform.position, enemyMovement.transform.position);

        }
        bool canMoveForward = distanceToEnemy > Vitrikedich;
        bool hasEnoughStamina = playerStamina.currentStamina >= 10;
        bool canSuperAttack = Vector3.Distance(enemyTransform.position, playerTransform.position) <= Vitrichemgan;
        bool canMoveBackward = playerTransform.position.x > leftBoundary;


        // Cập nhật trạng thái của các nút
        moveForwardButton.gameObject.SetActive(isPlayerTurn && canMoveForward && hasEnoughStamina);
        moveBackwardButton.gameObject.SetActive(isPlayerTurn && canMoveBackward && hasEnoughStamina);
        weakAttackButton.gameObject.SetActive(isPlayerTurn && hasEnoughStamina);
        normalAttackButton.gameObject.SetActive(canSuperAttack && isPlayerTurn && hasEnoughStamina);
        strongAttackButton.gameObject.SetActive(canSuperAttack && isPlayerTurn && hasEnoughStamina);
        restButton.gameObject.SetActive(isPlayerTurn && (playerStamina.currentStamina < playerStamina.maxStamina / 2 || !hasEnoughStamina));
    }
}
