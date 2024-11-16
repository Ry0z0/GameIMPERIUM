using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss1NormalManagement : MonoBehaviour
{
    public BaseBoss baseBoss = new BaseBoss();
    private EnemyMovement movement;
    public Image staminaBar;
    public TextMeshProUGUI enemyStaminaText;
    public Image healthBar;
    public TextMeshProUGUI enemyHealthText;
    public Image lose;
    private void Start()
    {
        if(this.gameObject.scene.name == "Boss1")
        {
            baseBoss.StartBoss1();
        }else if (this.gameObject.scene.name == "Boss2")
        {
            baseBoss.StartBoss2();
        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            baseBoss.StartBoss3();
        }


            // Thêm thành phần EnemyMovement vào GameObject
            movement = gameObject.AddComponent<EnemyMovement>();
        UpdateHealthBar();
        UpdateStaminaBar();

        if (staminaBar == null || enemyStaminaText == null || healthBar == null || enemyHealthText == null)
        {
            Debug.LogWarning("Một trong các thành phần UI không được thiết lập trong Inspector.");
        }
    }

    private void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (enemyStaminaText != null)
        {
            enemyStaminaText.text = $"{baseBoss.currentStamina}/{baseBoss.stamina}";
        }

        if (enemyHealthText != null)
        {
            enemyHealthText.text = $"{baseBoss.currentHp}/{baseBoss.hp}";
        }

        UpdateHealthBar();
        UpdateStaminaBar();

        Debug.Log($"UpdateUI is called - Current HP: {baseBoss.currentHp}, Max HP: {baseBoss.hp}, Current Stamina: {baseBoss.currentStamina}, Max Stamina: {baseBoss.stamina}");
    }


    public virtual void TakeDamage(int amount)
    {
        baseBoss.currentHp -= amount;
        baseBoss.currentHp = Mathf.Clamp(baseBoss.currentHp, 0, baseBoss.hp);  // Đảm bảo máu không âm
        Debug.Log("After taking damage, Current HP: " + baseBoss.currentHp);
        UpdateUI();  // Cập nhật giao diện người dùng
        if(baseBoss.currentHp <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Time.timeScale = 0;
        Debug.Log("Enemy is dead!");
        lose.gameObject.SetActive(true);
    }
    public void Rest()
    {
        RegainStamina(20);
    }

    private void RegainStamina(int amount)
    {
        baseBoss.currentStamina = Mathf.Min(baseBoss.currentStamina + amount, baseBoss.stamina);
        Debug.Log("After resting, Current Stamina: " + baseBoss.currentStamina);
        UpdateUI();  // Cập nhật giao diện người dùng
    }
    public void ReduceStamina(int amount)
    {
        baseBoss.currentStamina = Mathf.Max(baseBoss.currentStamina - amount, 0);
        UpdateUI();  // Cập nhật giao diện người dùng
    }
    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = (float)baseBoss.currentStamina / baseBoss.stamina;
            Debug.Log("Stamina Bar fillAmount: " + staminaBar.fillAmount);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)baseBoss.currentHp / baseBoss.hp;
            Debug.Log("Health Bar fillAmount: " + healthBar.fillAmount);
        }
    }
}
