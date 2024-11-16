using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class EnemyNormalAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Khoảng cách để tấn công
    private EnemyStats enemyStats = new EnemyStats();
    public PlayerHealth playerHealth;
    public PlayerDodge playerDodge;
    public PlayerDefense playerDefense;
    public EnemyNormalManagement enemyNormalManagement;
    private Transform playerTransform;
    private Animator animator;
    private string triggerAttackWeak = "BossLowCut";
    private string triggerAttackNormal = "BossNormalCut";
    private string triggerAttackStrong = "BossStrongCut";
    private string triggerDodge = "Block";
    public TextMeshProUGUI dameText;
    public TextMeshProUGUI dodgeText;
    public GameObject dame;
    public GameObject dodge;
    public GameObject otherGameObject; // Assign this in the Inspector
    private Animator otherAnimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (otherGameObject != null)
        {
            otherAnimator = otherGameObject.GetComponent<Animator>();
        }
        // Kiểm tra xem Player đã được gán chưa
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
        if (enemyNormalManagement != null)
        {
            enemyNormalManagement.stats.AssignRandomStrength();
            enemyNormalManagement.UpdateUI(); // Gọi UpdateUI qua boss1NormalManagement
        }
    }


    // Hàm tấn công yếu
    public void AttackWeak()
    {
        if (playerHealth != null && enemyNormalManagement.stats.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackWeak);
            if (!playerDodge.CanDodge(enemyNormalManagement.stats.attack, 6))
            {
                dame.SetActive(true);
                int damageDealt = Mathf.Max(enemyNormalManagement.stats.attack - playerDefense.playerDefense, 0);
                playerHealth.TakeDamage(10+damageDealt);
                dameText.text = $"{10 + damageDealt}";
            }
            else
            {
                otherAnimator.SetTrigger(triggerDodge);
                dodge.SetActive(true);
                dodgeText.text = "Dodged!";
            }
            StartCoroutine(HideGuideTextAfterDelay(1f));
            enemyNormalManagement.ReduceStamina(10);
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            enemyNormalManagement.Rest();
        }
    }

    // Hàm tấn công thường
    public void AttackNormal()
    {
        if (playerHealth != null && enemyNormalManagement.stats.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackNormal);
            if (!playerDodge.CanDodge(enemyNormalManagement.stats.attack, 8))
            {
                dame.SetActive(true);
                int damageDealt = Mathf.Max(enemyNormalManagement.stats.attack - playerDefense.playerDefense, 0);
                playerHealth.TakeDamage(15+damageDealt);
                dameText.text = $"{15 + damageDealt}";
            }
            else
            {
                otherAnimator.SetTrigger(triggerDodge);
                dodge.SetActive(true);
                dodgeText.text = "Dodged!";
            }
            StartCoroutine(HideGuideTextAfterDelay(1f));
            enemyNormalManagement.ReduceStamina(15);
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            enemyNormalManagement.Rest();
        }
    }

    // Hàm tấn công mạnh
    public void AttackStrong()
    {
        if (playerHealth != null && enemyNormalManagement.stats.currentStamina > 0)
        {
            animator.SetTrigger(triggerAttackStrong);
            if (!playerDodge.CanDodge(enemyNormalManagement.stats.attack, 10))
            {
                dame.SetActive(true);
                int damageDealt = Mathf.Max(enemyNormalManagement.stats.attack - playerDefense.playerDefense, 0);
                playerHealth.TakeDamage(20+damageDealt);
                dameText.text = $"{20 + damageDealt}";
            }
            else
            {
                otherAnimator.SetTrigger(triggerDodge);
                dodge.SetActive(true);
                dodgeText.text = "Dodged!";
            }
            StartCoroutine(HideGuideTextAfterDelay(1f));
            enemyNormalManagement.ReduceStamina(20);
        }
        else
        {
            Debug.Log("Enemy out of stamina, resting to regain stamina");
            enemyNormalManagement.Rest();
        }
    }

    private IEnumerator HideGuideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dameText.text = ""; // Ẩn text bằng cách xóa nội dung của nó
        dodgeText.text = "";
        dame.SetActive(false);
        dodge.SetActive(false);
    }

}
