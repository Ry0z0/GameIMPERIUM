using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTutorialStamina : MonoBehaviour
{
    public Image staminaBar;  // Tham chiếu đến thanh thể lực UI
    public float maxStamina = 100f; // Thể lực tối đa
    public float currentStamina;   // Thể lực hiện tại
    public TextMeshProUGUI enemyStaminaText;

    void Start()
    {
        currentStamina = maxStamina; // Đặt thể lực ban đầu là tối đa
        UpdateStaminaBar(); // Cập nhật giao diện thể lực
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (enemyStaminaText != null)
        {
            enemyStaminaText.text = currentStamina.ToString("F0") + "/" + maxStamina.ToString("F0");
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
        UpdateStaminaBar();
    }

    // Cập nhật thanh thể lực
    private void UpdateStaminaBar()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
    }
}
