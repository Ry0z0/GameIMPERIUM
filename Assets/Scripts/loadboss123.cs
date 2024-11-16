using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBossScenes : MonoBehaviour
{
    private int buttonPressCount = 0; // Biến đếm số lần nhấn nút
    private static bool isFirstTimeRun = true;
    void Start()
    {
        // Kiểm tra nếu chưa có buttonPressCount trong PlayerPrefs thì reset về 0
        if (isFirstTimeRun)
        {
            PlayerPrefs.SetInt("buttonPressCount", 0); // Đặt giá trị lần đầu tiên
            isFirstTimeRun = false;
        }

        // Lấy giá trị buttonPressCount từ PlayerPrefs
        buttonPressCount = PlayerPrefs.GetInt("buttonPressCount", 0);
    }

    // Hàm này sẽ được gọi khi nhấn nút
    public void OnButtonClick()
    {
        buttonPressCount++; // Tăng biến đếm mỗi khi nhấn nút
        PlayerPrefs.SetInt("buttonPressCount", buttonPressCount); // Lưu lại giá trị vào PlayerPrefs

        switch (buttonPressCount)
        {
            case 1:
                SceneManager.LoadScene("Boss1"); // Chuyển đến cảnh Boss 1
                break;
            case 2:
                SceneManager.LoadScene("Boss2"); // Chuyển đến cảnh Boss 2
                break;
            case 3:
                SceneManager.LoadScene("Boss3"); // Chuyển đến cảnh Boss 3
                break;
            default:
                buttonPressCount = 0; // Reset lại biến đếm
                PlayerPrefs.SetInt("buttonPressCount", buttonPressCount); // Cập nhật lại giá trị trong PlayerPrefs
                break;
        }
    }
}
