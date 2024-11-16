using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public int dodgeChance;
	public int maxHealth;
    public int currentHealth;
    public Image healthBar;
    public TextMeshProUGUI palyerHealthText;
    private string saveFilePath;
    public Image win;

    private void Start()
    {
        // Lấy đường dẫn thư mục lưu trữ dựa trên Application.persistentDataPath
        string directoryPath = Application.persistentDataPath + "/DB";

        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Đường dẫn tới file JSON
        saveFilePath = directoryPath + "/playerData.json";
        Debug.Log("File Path: " + saveFilePath);
        currentHealth = maxHealth;
        LoadPlayerData();
        UpdateHealthUI();

    }
	public bool CanDodge()
	{
		int randomChance = Random.Range(0, 100);
		return randomChance < dodgeChance;
	}
	private void Update()
    {
        UpdateUI();
        Debug.Log(currentHealth);
    }

    private void UpdateUI()
    {
        if (palyerHealthText != null)
        {
            palyerHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
        }

    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  // Đảm bảo máu không âm
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        win.gameObject.SetActive(true);
    }

    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            maxHealth = 100 + data.hp; // Thiết lập maxHealth bằng 100 + giá trị hp từ file JSON
            currentHealth = maxHealth; // Ban đầu giá trị máu hiện tại là tối đa

            Debug.Log("Player HP loaded successfully: " + currentHealth);
        }
        else
        {
            Debug.LogError("Save file not found at path: " + saveFilePath);
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int attack;
        public int defense;
        public int hp;
        public int stamina;
        public int dodge;
        public int skillPoints;
    }
}
