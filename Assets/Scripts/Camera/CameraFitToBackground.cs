using UnityEngine;

public class CameraFitToBackground : MonoBehaviour
{
    public SpriteRenderer background;  // Gắn background sprite vào đây

    void Start()
    {
        AdjustCameraToBackground();
    }

    void AdjustCameraToBackground()
    {
        Camera.main.orthographic = true;

        // Lấy chiều cao và chiều rộng của background
        float backgroundHeight = background.bounds.size.y;
        float backgroundWidth = background.bounds.size.x;

        // Lấy tỷ lệ khung hình của màn hình
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = backgroundWidth / backgroundHeight;

        if (screenRatio >= targetRatio)
        {
            // Nếu tỷ lệ màn hình rộng hơn, điều chỉnh theo chiều cao
            Camera.main.orthographicSize = backgroundHeight / 2;
        }
        else
        {
            // Nếu tỷ lệ màn hình hẹp hơn, điều chỉnh theo chiều rộng
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = backgroundHeight / 2 * differenceInSize;
        }
    }
}

