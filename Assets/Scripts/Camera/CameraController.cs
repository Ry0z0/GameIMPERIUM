using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // Nhân vật player
    public Transform enemy;   // Nhân vật enemy
    public Camera mainCamera; // Camera chính

    public float minZoom = 1f;  // Giá trị Orthographic Size nhỏ nhất
    public float maxZoom = 5f;  // Giá trị Orthographic Size lớn nhất (phù hợp với background)
    public float zoomSpeed = 5f; // Tốc độ zoom
    public float maxDistance = 10f;  // Khoảng cách tối đa giữa player và enemy mà camera sẽ zoom out hết cỡ


    void Update()
    {
        // Tính khoảng cách giữa player và enemy
        float distance = Vector3.Distance(player.position, enemy.position);

        // Tính zoom tỷ lệ thuận với khoảng cách, giữa giá trị minZoom và maxZoom
        float zoom = Mathf.Lerp(minZoom, maxZoom, distance / maxDistance);

        // Thay đổi Orthographic Size của camera theo khoảng cách
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoom, Time.deltaTime * zoomSpeed);

        // Đảm bảo camera không phóng to quá background
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
    }
}
