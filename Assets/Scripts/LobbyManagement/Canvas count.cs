using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvascount : MonoBehaviour
{
    public float displayTime = 3f; // Thời gian hiển thị của Canvas
    private static bool hasCanvasShown = false; // Biến static để kiểm tra trạng thái

    private void Start()
    {
        // Kiểm tra nếu Canvas đã được hiển thị trước đó
        if (hasCanvasShown)
        {
            // Nếu Canvas đã hiển thị, thì ẩn ngay lập tức
            gameObject.SetActive(false);
        }
        else
        {
            // Nếu chưa hiển thị, đặt cờ và hiển thị Canvas
            hasCanvasShown = true;
            Invoke("HideCanvas", displayTime);
        }
    }

    void HideCanvas()
    {
        // Ẩn Canvas sau thời gian quy định
        gameObject.SetActive(false);
    }

}
