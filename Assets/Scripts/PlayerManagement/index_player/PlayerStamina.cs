using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public Image staminaBar;  // Tham chiếu đến thanh thể lực UI
    public float maxStamina; // Thể lực tối đa
    public float currentStamina;   // Thể lực hiện tại
    public TextMeshProUGUI playerStaminaText;
    private string saveFilePath;
    public AudioSource staminaSound;

    void Start()
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
        currentStamina = maxStamina;
        LoadPlayerData();
        UpdateStaminaBar(); // Cập nhật giao diện thể lực
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerStaminaText != null)
        {
            playerStaminaText.text = currentStamina.ToString("F0") + "/" + maxStamina.ToString("F0");
        }
    }

    // Giảm thể lực
    public void ReduceStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0f)
        {
            currentStamina = 0f;
        }
        UpdateStaminaBar();
    }

    // Hồi thể lực
    public void RegainStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        staminaSound.Play();
        UpdateStaminaBar();
    }

    // Cập nhật thanh thể lực
    private void UpdateStaminaBar()
    {
        
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Thiết lập maxStamina bằng 100 + giá trị stamina từ file JSON
            maxStamina = 100 + data.stamina;
            currentStamina = maxStamina; // Ban đầu giá trị thể lực hiện tại là tối đa
            Debug.Log("Player Stamina loaded successfully: " + currentStamina);
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
        public int stamina;  // Thể lực của người chơi
        public int dodge;
        public int skillPoints;
    }
}

        