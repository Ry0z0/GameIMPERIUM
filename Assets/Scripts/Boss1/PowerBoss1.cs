using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PowerBoss1 : MonoBehaviour
{
  public float attackRange = 2.0f; // Khoảng cách để tấn công
	public PlayerHealth playerHealth;
	public PlayerDodge playerDodge;
	public Boss1NormalManagement boss1NormalManagement;
	private Transform playerTransform;
	private Animator animator;
	private string triggerAttackWeak = "BossLowCut";
	private string triggerAttackNormal = "BossNormalCut";
	private string triggerAttackStrong = "BossStrongCut";
	private string triggerDodged = "Block";
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

		if (boss1NormalManagement != null)
		{
			boss1NormalManagement.baseBoss.StartBoss1();
			boss1NormalManagement.UpdateUI(); // Gọi UpdateUI qua boss1NormalManagement
		}
	}

	// Hàm tấn công yếu
	public void AttackWeak()
	{
		if (playerHealth != null && boss1NormalManagement.baseBoss.currentStamina > 0)
		{
			animator.SetTrigger(triggerAttackWeak);
			if (!playerDodge.CanDodge(boss1NormalManagement.baseBoss.attack, 6))
			{
				dame.SetActive(true);
				int damageDealt = Mathf.Max(boss1NormalManagement.baseBoss.attack - boss1NormalManagement.baseBoss.defense, 0);
				playerHealth.TakeDamage(10 + damageDealt);
				dameText.text = $"{10 + damageDealt}";
			}
			else
			{
				otherAnimator.SetTrigger(triggerDodged);
				dodge.SetActive(true);
				dodgeText.text = "Dodged!";
			}
			StartCoroutine(HideGuideTextAfterDelay(1f));
			boss1NormalManagement.ReduceStamina(10);
		}
		else
		{
			Debug.Log("Enemy out of stamina, resting to regain stamina");
			boss1NormalManagement.Rest();
		}
	}

	// Hàm tấn công thường
	public void AttackNormal()
	{
		if (playerHealth != null && boss1NormalManagement.baseBoss.currentStamina > 0)
		{
			animator.SetTrigger(triggerAttackNormal);
			if (!playerDodge.CanDodge(boss1NormalManagement.baseBoss.attack, 8))
			{
				dame.SetActive(true);
				int damageDealt = Mathf.Max(boss1NormalManagement.baseBoss.attack - boss1NormalManagement.baseBoss.defense, 0);
				playerHealth.TakeDamage(15 + damageDealt);
				dameText.text = $"{15 + damageDealt}";
			}
			else
			{
				otherAnimator.SetTrigger(triggerDodged);
				dodge.SetActive(true);
				dodgeText.text = "Dodged!";
			}
			StartCoroutine(HideGuideTextAfterDelay(1f));
			boss1NormalManagement.ReduceStamina(10);
		}
		else
		{
			Debug.Log("Enemy out of stamina, resting to regain stamina");
			boss1NormalManagement.Rest();
		}
	}

	// Hàm tấn công mạnh
	public void AttackStrong()
	{
		if (playerHealth != null && boss1NormalManagement.baseBoss.currentStamina > 0)
		{
			animator.SetTrigger(triggerAttackStrong);
			if (!playerDodge.CanDodge(boss1NormalManagement.baseBoss.attack, 10))
			{
				dame.SetActive(true);
				int damageDealt = Mathf.Max(boss1NormalManagement.baseBoss.attack - boss1NormalManagement.baseBoss.defense, 0);
				playerHealth.TakeDamage(20 + damageDealt);
				dameText.text = $"{20 + damageDealt}";
			}
			else
			{
				otherAnimator.SetTrigger(triggerDodged);
				dodge.SetActive(true);
				dodgeText.text = "Dodged!";
			}
			StartCoroutine(HideGuideTextAfterDelay(1f));
			boss1NormalManagement.ReduceStamina(10);
		}
		else
		{
			Debug.Log("Enemy out of stamina, resting to regain stamina");
			boss1NormalManagement.Rest();
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
