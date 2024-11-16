using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyTutorialAttack : MonoBehaviour
{
    public int attackDamage = 10;  // Sát thương của Enemy
    public float attackRange = 2.0f;
    public PlayerHealth playerHealth;  // Tham chiếu đến sức khỏe của Player
    public EnemyTutorialStamina enemyStamina;
    private Animator animator;
    private Transform playerTransform;
    private string triggerAttackWeak = "BossNormalCut";
    public TextMeshProUGUI dameText;
    public GameObject dame;

    private void Start()
    {
        if (playerHealth != null)
        {
            playerTransform = playerHealth.transform;
        }
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (playerHealth != null && enemyStamina.currentStamina > 0)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= attackRange)
            {
                animator.SetTrigger(triggerAttackWeak);
                // Nếu đứng gần đủ, tấn công và trừ máu
                dame.SetActive(true);
                playerHealth.TakeDamage(attackDamage);
                dameText.text = $"{attackDamage}";
            }
            else
            {
                Debug.Log("Enemy attempted to attack, but is too far away.");
            }
            StartCoroutine(HideGuideTextAfterDelay(1f));
            enemyStamina.ReduceStamina(15);
        }
        else
        {
            Debug.Log("Enemy does not have enough stamina to attack.");
            enemyStamina.ReduceStamina(20);
        }
    }

    private IEnumerator HideGuideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dameText.text = ""; // Ẩn text bằng cách xóa nội dung của nó
        dame.SetActive(false);
    }
}
