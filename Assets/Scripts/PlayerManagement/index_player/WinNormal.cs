using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Thư viện để chuyển Scene
using System.IO;  // Thư viện để làm việc với file

public class WinNormal : MonoBehaviour
{
    private string saveFilePath;

    void Start()
    {
        // Lấy đường dẫn tới file JSON
        string directoryPath = Application.persistentDataPath + "/DB";

        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Đường dẫn tới file JSON
        saveFilePath = directoryPath + "/playerData.json";
    }

    // Hàm được gọi khi nhấn vào button
    public void OnButtonPress()
    {
        // Cộng 2 điểm skillPoints và lưu lại
        UpdateSkillPoints();

        // Chuyển sang Scene thuộc tính nhân vật (thay "CharacterScene" bằng tên Scene thực tế của bạn)
        SceneManager.LoadScene("Thuoctinhnhanvat");
    }
    public void OnButtonBossPress()
    {
        // Cộng 2 điểm skillPoints và lưu lại
        UpdateSkill5Points();

        // Chuyển sang Scene thuộc tính nhân vật (thay "CharacterScene" bằng tên Scene thực tế của bạn)
        SceneManager.LoadScene("Thuoctinhnhanvat");
    }
    // Hàm để cập nhật skillPoints trong file JSON
    private void UpdateSkillPoints()
    {
        if (File.Exists(saveFilePath))
        {
            // Đọc file JSON
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Cộng thêm 2 điểm kỹ năng (skillPoints)
            data.skillPoints += 5;

            // Chuyển đổi đối tượng PlayerData thành JSON và ghi lại vào file
            string updatedJson = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFilePath, updatedJson);

            Debug.Log("SkillPoints updated: " + data.skillPoints);
        }
        else
        {
            Debug.LogError("Save file not found at path: " + saveFilePath);
        }
    }
    private void UpdateSkill5Points()
    {
        if (File.Exists(saveFilePath))
        {
            // Đọc file JSON
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Cộng thêm 2 điểm kỹ năng (skillPoints)
            data.skillPoints += 10;

            // Chuyển đổi đối tượng PlayerData thành JSON và ghi lại vào file
            string updatedJson = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFilePath, updatedJson);

            Debug.Log("SkillPoints updated: " + data.skillPoints);
        }
        else
        {
            Debug.LogError("Save file not found at path: " + saveFilePath);
        }
    }
    // Lớp PlayerData để ánh xạ với dữ liệu JSON
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
