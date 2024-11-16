using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyNormalManagement : MonoBehaviour
{
    public EnemyStats stats;
    private EnemyMovement movement;
    public Image staminaBar;
    public TextMeshProUGUI enemyStaminaText;
    public Image healthBar;
    public TextMeshProUGUI enemyHealthText;
    public Image lose;
    public Text bossAttackText;
    public Text bossHpText;
    public Text bossStaminaText;
    public Text bossDefendText;
    public Text bossDodgeText;
    private void Start()
    {
        // Giả sử `stats` đã được random từ trước, ta chỉ cần cập nhật UI
        if (stats == null)
        {
            Debug.LogError("stats chưa được khởi tạo và random.");
            return;
        }
        Debug.Log("Assigned Stats: Attack=" + stats.attack + ", HP=" + stats.hp +
              ", Stamina=" + stats.stamina + ", Defense=" + stats.defense +
              ", Dodge=" + stats.dodge);

        // Thêm component EnemyMovement vào GameObject
        movement = gameObject.AddComponent<EnemyMovement>();

        // Cập nhật thanh máu và thanh stamina
        UpdateHealthBar();
        UpdateStaminaBar();

        // Cập nhật UI với các chỉ số của boss
        UpdateBossStatsUI();
    }

    private void Update()
    {
        UpdateBossStatsUI();
    }
    void UpdateBossStatsUI()
    {
        // Sử dụng các giá trị đã random từ `EnemyStats` để cập nhật UI
        bossAttackText.text = (10 + stats.attack) + " :Attack";
        bossHpText.text = stats.hp + " :HP";
        bossStaminaText.text = stats.stamina + " :Stamina";
        bossDefendText.text = stats.defense + " :Defense";
        bossDodgeText.text = stats.dodge + " :Dodge";
    }
    public void ReloadScene()
    {
        // Reset lại scene hiện tại bằng cách load lại chính nó
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateUI()
    {
        if (enemyStaminaText != null)
        {
            enemyStaminaText.text = $"{stats.currentStamina:F0}/{stats.stamina:F0}";
        }

        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"{stats.currentHp:F0}/{stats.hp:F0}";
        }
        UpdateHealthBar();
        UpdateStaminaBar();
    }
    public void Rest()
    {
        RegainStamina(20);
    }

    private void RegainStamina(int amount)
    {
        stats.currentStamina = Mathf.Min(stats.currentStamina + amount, stats.stamina);
        Debug.Log("After resting, Current Stamina: " + stats.currentStamina);
        UpdateUI();  // Cập nhật giao diện người dùng
    }
    public void ReduceStamina(int amount)
    {
        stats.currentStamina = Mathf.Max(stats.currentStamina - amount, 0);
        UpdateUI();  // Cập nhật giao diện người dùng
    }

    public virtual void TakeDamage(int amount)
    {
        stats.currentHp -= amount;
        stats.currentHp = Mathf.Clamp(stats.currentHp, 0, stats.hp);  // Đảm bảo máu không âm
        Debug.Log("After taking damage, Current HP: " + stats.currentHp);
        UpdateUI();  // Cập nhật giao diện người dùng
        if (stats.currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        lose.gameObject.SetActive(true);
    }

    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = (float)stats.currentStamina / stats.stamina;
            Debug.Log("Stamina Bar fillAmount: " + staminaBar.fillAmount);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)stats.currentHp / stats.hp;
            Debug.Log("Health Bar fillAmount: " + healthBar.fillAmount);
        }
    }
}
