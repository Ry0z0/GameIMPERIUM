using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    public GameObject player;  // Nhân vật chính
    public RectTransform moveForwardButton;  // Nút Tiến lên
    public RectTransform moveBackwardButton;  // Nút Lùi lại
    public RectTransform weakAttackButton;  // Nút Tấn công yếu
    public RectTransform normalAttackButton;  // Nút Tấn công thường
    public RectTransform strongAttackButton;  // Nút Tấn công mạnh
    public RectTransform restButton;
    public RectTransform dame;
    public RectTransform dodge;

    public float radius = 250f;

    void Update()
    {
        // Lấy vị trí của nhân vật trên màn hình
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(player.transform.position);

        // Vị trí nút Lùi lại (bên trái ngoài cùng)
        moveBackwardButton.position = screenPosition + new Vector3(-radius / 1.4f, -radius / 1.4f, 0);

        // Vị trí nút Nghỉ ngơi (bên trên bên trái)
        restButton.position = screenPosition + new Vector3(-radius / 1.4f, radius / 1.4f, 0);

        // Vị trí nút Tấn công mạnh (ở đỉnh đầu)
        strongAttackButton.position = screenPosition + new Vector3(0, radius, 0);

        // Vị trí nút Tấn công thường (bên trên bên phải)
        normalAttackButton.position = screenPosition + new Vector3(radius / 1.4f, radius / 1.4f, 0);

        dame.position = screenPosition + new Vector3(10+radius / 1.4f, radius / 1.4f, 0);

        dodge.position = screenPosition + new Vector3(10+radius / 1.4f, radius / 1.4f, 0);

        // Vị trí nút Tấn công yếu (phía dưới)
        weakAttackButton.position = screenPosition + new Vector3(radius, 0 , 0);

        // Vị trí nút Tiến lên (dưới cùng bên phải)
        moveForwardButton.position = screenPosition + new Vector3(radius / 1.4f, -radius / 1.4f, 0);
    }
}
