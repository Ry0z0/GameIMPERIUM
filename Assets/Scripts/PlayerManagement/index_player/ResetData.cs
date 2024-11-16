using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResetData : MonoBehaviour
{
    private string saveFilePath;

    // Cấu trúc dữ liệu người chơi
    [System.Serializable]
    public class PlayerData
    {
        public string playerName = "";
        public int attack = 1;
        public int defense = 1;
        public int hp = 1;
        public int stamina = 1;
        public int dodge = 1;
        public int skillPoints = 9;
    }

    public PlayerData playerData = new PlayerData();

    private void Start()
    {
        // Đường dẫn lưu file trong thư mục lưu trữ của ứng dụng
        string directoryPath = Application.persistentDataPath + "/DB";

        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        saveFilePath = directoryPath + "/playerData.json";
        Debug.Log("File Path: " + saveFilePath);

        // Tải dữ liệu khi bắt đầu
        LoadData();
    }

    // Hàm để tải dữ liệu từ file
    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Data Loaded Successfully");
        }
        else
        {
            Debug.Log("Save file not found at path: " + saveFilePath);
        }
    }

    // Hàm để reset dữ liệu về giá trị mặc định và xóa file lưu trữ
    public void ResetDataPlayer()
    {
        // Xóa file lưu trữ nếu tồn tại
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }

        // Đặt lại giá trị mặc định cho playerData
        playerData = new PlayerData();
        Debug.Log("Data Reset to Default Values");
    }
}
