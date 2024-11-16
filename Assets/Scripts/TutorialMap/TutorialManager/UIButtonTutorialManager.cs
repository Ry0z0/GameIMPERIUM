using UnityEngine;
using UnityEngine.UI;

public class UIButtonTutorialManager : MonoBehaviour
{
    public Button moveForwardButton;
    public Button moveBackwardButton;
    public Button weakAttackButton;
    public Button normalAttackButton;
    public Button strongAttackButton;
    public Button restButton;
    private TurnTutorialManager turnManager;
    private PlayerStamina playerStamina;
    private PlayerMovementV2 playerMovement;
    private PlayerAttack playerAttack;
    private Transform playerTransform;
    private EnemyTutorialMovement enemyMovement;
    private TutorialManager tutorialManager;
    public float leftBoundary;
    public float rightBoundary;
    public float Vitrikedich = 1f;

  
    void Start()
    {
        leftBoundary = -((1080 / 2) / 100f);
        rightBoundary = ((1080 / 2) / 100f);

        turnManager = FindObjectOfType<TurnTutorialManager>();
        playerMovement = FindObjectOfType<PlayerMovementV2>();
        playerStamina = playerMovement.GetComponent<PlayerStamina>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        playerTransform = playerMovement.transform;
        enemyMovement = FindObjectOfType<EnemyTutorialMovement>();
        tutorialManager = FindObjectOfType<TutorialManager>();

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
        float distanceToEnemy = Vector3.Distance(playerTransform.position, enemyMovement.transform.position);
        bool hasEnoughStamina = playerStamina.currentStamina >= 10;
        bool canMoveForward = distanceToEnemy > Vitrikedich;
        bool canMoveBackward = playerTransform.position.x > leftBoundary;
        int currentStep = tutorialManager.GetCurrentStep();
        if(playerStamina.currentStamina < playerStamina.maxStamina / 2 && currentStep == 2)
        {
            tutorialManager.SetCurrentStep(3);
        }

        // Cập nhật trạng thái của các nút
        moveForwardButton.gameObject.SetActive(isPlayerTurn && canMoveForward && hasEnoughStamina && currentStep>=1 && currentStep<4);
        moveBackwardButton.gameObject.SetActive(isPlayerTurn && canMoveBackward && hasEnoughStamina && currentStep >= 1 && currentStep < 4);
        weakAttackButton.gameObject.SetActive(isPlayerTurn && hasEnoughStamina && currentStep >= 4 && currentStep < 6);
        normalAttackButton.gameObject.SetActive(isPlayerTurn && hasEnoughStamina && currentStep >= 5 && currentStep < 6);
        strongAttackButton.gameObject.SetActive(isPlayerTurn && hasEnoughStamina && currentStep >= 5 && currentStep < 6);
        restButton.gameObject.SetActive(isPlayerTurn && (playerStamina.currentStamina < playerStamina.maxStamina / 2 || !hasEnoughStamina) && currentStep < 6);
    }
}
