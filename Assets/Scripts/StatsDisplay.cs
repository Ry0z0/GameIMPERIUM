using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StatsDisplay : MonoBehaviour
{
    // References to UI Text components for Player stats
    public Text playerAttackText;
    public Text playerHpText;
    public Text playerStaminaText;
    public Text playerDefendText;
    public Text playerDodgeText;

    public Text bossAttackText;
    public Text bossHpText;
    public Text bossStaminaText;
    public Text bossDefendText;
    public Text bossDodgeText;

    private BaseBoss boss;
    private EnemyStats enemyStats;
    // Placeholder variables for player stats
    private int playerAttack;
    private int playerHp;
    private int playerStamina;
    private int playerDefend;
    private int playerDodge;
    private string saveFilePath;

    void Start()
    {
        // Set the directory path and ensure it exists
        string directoryPath = Application.persistentDataPath + "/DB";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Set the save file path
        saveFilePath = directoryPath + "/playerData.json";
        Debug.Log("Save file path: " + saveFilePath); // Print the path for verification

        // Load player data from file
        LoadPlayerData();

        // Initial update of the UI
        UpdateStatsUI();
        // Khởi tạo đối tượng Boss (hoặc nhận từ một script khác)
        boss = new BaseBoss();
        if (this.gameObject.scene.name == "Boss1")
        {
            boss.StartBoss1();
        }
        else if (this.gameObject.scene.name == "Boss2")
        {
            boss.StartBoss2();
        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            boss.StartBoss3();
        }
        // Cập nhật UI với các giá trị của Boss
        UpdateBossStatsUI();
    }

    void UpdateBossStatsUI()
    {
            // Cập nhật các giá trị từ BaseBoss lên UI
            bossAttackText.text =boss.attack + " :Attack";
            bossHpText.text = boss.hp + " :HP";
            bossStaminaText.text = boss.stamina + " :Stamina";
            bossDefendText.text = boss.defense + " :Defense";
            bossDodgeText.text = boss.dodge + " :Dodge";
    }

    void UpdateStatsUI()
    {
        // Update player stats UI
        playerAttackText.text = "Attack: " + (10 + playerAttack);
        playerHpText.text = "HP: " + (100 + playerHp);
        playerStaminaText.text = "Stamina: " + (100 + playerStamina);
        playerDefendText.text = "Defense: " + playerDefend;
        playerDodgeText.text = "Dodge: " + playerDodge;
    }

    // Method to load player stats from a file
    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            Debug.Log("JSON Content: " + json); // Kiểm tra nội dung JSON

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Set player stats from loaded data
            playerAttack = data.attack;
            playerHp = data.hp;
            playerStamina = data.stamina;
            playerDefend = data.defense;
            playerDodge = data.dodge;

            // In ra các giá trị đã load để kiểm tra
            Debug.Log("Player data loaded successfully:");
            Debug.Log("Attack: " + playerAttack);
            Debug.Log("HP: " + playerHp);
            Debug.Log("Stamina: " + playerStamina);
            Debug.Log("Defense: " + playerDefend);
            Debug.Log("Dodge: " + playerDodge);

            UpdateStatsUI(); // Cập nhật UI với dữ liệu mới
        }
        else
        {
            Debug.LogWarning("Save file not found at path: " + saveFilePath);
        }
    }

    // Data structure for saving/loading player data
    [System.Serializable]
    public class PlayerData
    {
        public int attack;
        public int defense;
        public int hp;
        public int stamina;
        public int dodge;
    }
}
