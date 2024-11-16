using UnityEngine;

public class TurnNormalManager : MonoBehaviour
{
    public PlayerMovementV2 playerMovement;
    public PlayerHealth playerHealth;
    private EnemyNormalAIController enemyNormalAIController;
    public EnemyStats enemyNormalHealth;
    private Boss1AIController boss1AIController;
    //public SceneController sceneController;

    public bool isPlayerTurn = true;
    private bool isEnemyTurn = false;

    void Start()
    {
        enemyNormalAIController = FindObjectOfType<EnemyNormalAIController>();
        boss1AIController = FindObjectOfType<Boss1AIController>();
        StartPlayerTurn(); // Bắt đầu với lượt của người chơi
    }

    public bool IsPlayerTurn
    {
        get { return isPlayerTurn; }
        set { isPlayerTurn = value; }
    }

    void StartPlayerTurn()
    {
        if (CheckGameOver() || isEnemyTurn == true) return; // Kiểm tra nếu đang xử lý lượt hoặc đã GameOver
        isPlayerTurn = true;
        Debug.Log("Player's turn started");
    }
    public void EndPlayerTurn()
    {
        if (CheckGameOver() || isEnemyTurn == true || isPlayerTurn == true) return;
        isPlayerTurn = false;
        isEnemyTurn = true;  // Đặt cờ đang xử lý lượt
        Debug.Log("Player's turn ended, AI's turn starting...");

        Invoke(nameof(StartEnemyTurn), 1.5f); // Chuyển lượt cho AI

    }

    void StartEnemyTurn()
    {
        if (CheckGameOver() || isEnemyTurn == false) return; // Kiểm tra nếu đang xử lý lượt hoặc đã GameOver
        if (this.gameObject.scene.name == "Boss1")
        {
            Debug.Log("AI Normal's turn started");
            boss1AIController.MakeDecision(); // AI cho map Normal
            Invoke(nameof(EndEnemyTurn), 1.5f); // Sau một khoảng thời gian, kết thúc lượt của AI}

        }
        else if (this.gameObject.scene.name == "Boss2")
        {
            Debug.Log("AI Normal's turn started");
            boss1AIController.MakeDecision(); // AI cho map Normal
            Invoke(nameof(EndEnemyTurn), 1.5f); // Sau một khoảng thời gian, kết thúc lượt của AI}

        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            Debug.Log("AI Normal's turn started");
            boss1AIController.MakeDecision(); // AI cho map Normal
            Invoke(nameof(EndEnemyTurn), 1.5f); // Sau một khoảng thời gian, kết thúc lượt của AI}

        }
        else
        {
            Debug.Log("AI Normal's turn started");
            enemyNormalAIController.MakeDecision(); // AI cho map Normal
            Invoke(nameof(EndEnemyTurn), 1.5f); // Sau một khoảng thời gian, kết thúc lượt của AI}
        }
    }
    void EndEnemyTurn()
    {
        if (CheckGameOver()) return; // Kiểm tra GameOver sau lượt của AI
        isEnemyTurn = false;  // Đặt lại cờ khi lượt kết thúc
        Debug.Log("AI Normal's turn ended, Player's turn starting...");
        StartPlayerTurn(); // Quay lại lượt của người chơi
    }


    // Kiểm tra điều kiện kết thúc game
    bool CheckGameOver()
    {
        if (playerHealth.currentHealth <= 0)
        {
            Debug.Log("Player is dead. Game Over!");
            return true;
        }

        //if (player.IsFirstTimeRun && player != null)
        //{
        //    if (enemyTutorialHealth.currentHealth <= 0)
        //    {
        //        Debug.Log("Enemy is dead. Game Over!");
        //        return true;
        //    }
        //}
        //else
        //{
        //    if (enemyNormalHealth.hp <= 0 && enemyNormalHealth != null)
        //    {
        //        Debug.Log("Enemy is dead. Game Over!");
        //        return true;
        //    }
        //}

        return false;
    }
}

