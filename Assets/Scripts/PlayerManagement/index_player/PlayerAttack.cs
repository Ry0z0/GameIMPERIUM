using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage;  // Sát thương của Player (sẽ được load từ dữ liệu)
    public float attackRange = 1.6f; // Khoảng cách để tấn công
    public PlayerStamina playerStamina;
    private EnemyTutorialHealth enemyHealth;
    private Transform enemyTransform;
    private Animator animator;
    private PlayerMovementV2 playerMovementV2;
    private string triggerAttackWeak = "Cut_Right";
    private string triggerAttackNormal = "CutNormal";
    private string triggerAttackStrong = "StrongCut";
    private string triggerDodged = "BossBlock";
    private TutorialManager tutorialManager;
    private EnemyStats enemyStats = new EnemyStats();
    private EnemyNormalManagement enemyNormalManagement;
    private BaseBoss baseBoss = new BaseBoss();
    private Boss1NormalManagement boss1NormalManagement;
    public TextMeshProUGUI dameText;
    public TextMeshProUGUI dodgeText;
    public GameObject dame;
    public GameObject dodge;
	public GameObject otherGameObject; // Assign this in the Inspector
	private Animator otherAnimator;
	private string saveFilePath;
    float distanceToEnemy;
    public AudioSource attackSound;
    public AudioSource dodgeSound;

    private void Start()
    {
		animator = GetComponent<Animator>();
		if (otherGameObject != null)
		{
			otherAnimator = otherGameObject.GetComponent<Animator>();
		}
		// Tìm đường dẫn file JSON để load chỉ số
		string directoryPath = Application.persistentDataPath + "/DB";
        saveFilePath = directoryPath + "/playerData.json";

        // Load dữ liệu của nhân vật
        LoadPlayerData();
        animator = GetComponent<Animator>();
        tutorialManager = FindObjectOfType<TutorialManager>();
        enemyHealth = FindObjectOfType<EnemyTutorialHealth>();
        playerStamina = FindObjectOfType<PlayerStamina>();
        enemyNormalManagement = FindObjectOfType<EnemyNormalManagement>();
        boss1NormalManagement = FindObjectOfType<Boss1NormalManagement>();
        playerMovementV2 = FindObjectOfType<PlayerMovementV2>();
       
        enemyTransform = GameObject.FindWithTag("enemy").transform;
        // Kiểm tra xem Enemy đã được gán chưa
        if (enemyHealth != null)
        {
            enemyTransform = enemyHealth.transform;
        }
    }

    private void Update()
    {
        distanceToEnemy = playerMovementV2.distance;
        if (enemyTransform != null && this.gameObject.scene.name == "TurtorialMap")
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
            if (distanceToEnemy <= attackRange && tutorialManager.GetCurrentStep() == 3)
            {
                tutorialManager.SetCurrentStep(4);
            }

            if (enemyHealth != null && enemyHealth.currentHealth <= enemyHealth.maxHealth * 0.3f && tutorialManager.GetCurrentStep() == 4)
            {
                tutorialManager.SetCurrentStep(5);
            }
        }
        
        Debug.Log($"Distance to enemy: {distanceToEnemy}, Attack Range: {attackRange}");

    }


    // Hàm tấn công yếu
    public void AttackWeak()
    {
        if (this.gameObject.scene.name == "TurtorialMap")
        {
            if (enemyHealth != null && playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackWeak);
                // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                if (!enemyHealth.CanDodge())
                {
                    // Kẻ địch không né được, tiến hành tấn công
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(10 + attackDamage - enemyHealth.defense, 0); // Trừ phòng thủ của địch
                    enemyHealth.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";

                }
                else
                {
                    otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                // Trừ stamina sau khi tấn công
                playerStamina.ReduceStamina(10);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "NormalMap")
        {
            // Lệnh kiểm tra nếu đứng gần thì đánh, nếu đứng xa thì thành chém xa
            if (distanceToEnemy <= attackRange)
            {
                if (playerStamina.currentStamina > 0)
                {
                    animator.SetTrigger(triggerAttackWeak);
                    // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                    if (!enemyStats.CanDodge(10 + attackDamage, 6))
                    {
                        // Kẻ địch không né được, tiến hành tấn công
                        dame.SetActive(true);
                        attackSound.Play();
                        int damageDealt = Mathf.Max(10 + attackDamage - enemyNormalManagement.stats.defense, 0); // Trừ phòng thủ của địch
                        enemyNormalManagement.TakeDamage(damageDealt);
                        dameText.text = $"{damageDealt}";
                    }
                    else
                    {
						otherAnimator.SetTrigger(triggerDodged);
                        dodgeSound.Play();
                        dodge.SetActive(true);
                        dodgeText.text = "Dodged!";
                    }
                    StartCoroutine(HideGuideTextAfterDelay(1f));
                    // Trừ stamina sau khi tấn công
                    playerStamina.ReduceStamina(10);
                }
                else
                {
                    Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                    playerStamina.RegainStamina(20);
                }
            }
            else
            {
                animator.SetTrigger(triggerAttackWeak);
                playerStamina.ReduceStamina(10);
            }
        }

        else if (this.gameObject.scene.name == "Boss1")
        {
            if (distanceToEnemy <= attackRange)
            {
                if (playerStamina.currentStamina > 0)
                {
                    animator.SetTrigger(triggerAttackWeak);
                    // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                    if (!boss1NormalManagement.baseBoss.CanDodge(10 + attackDamage, 6))
                    {
                        dame.SetActive(true);
                        attackSound.Play();
                        int damageDealt = Mathf.Max(10 + attackDamage - boss1NormalManagement.baseBoss.defense, 0); // Trừ phòng thủ của địch
                        boss1NormalManagement.TakeDamage(damageDealt);
                        dameText.text = $"{damageDealt}";
                    }
                    else
                    {
						otherAnimator.SetTrigger(triggerDodged);
                        dodgeSound.Play();
                        dodge.SetActive(true);
                        dodgeText.text = "Dodged!";
                    }
                    StartCoroutine(HideGuideTextAfterDelay(1f));
                    // Trừ stamina sau khi tấn công
                    playerStamina.ReduceStamina(10);
                }
                else
                {
                    Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                    playerStamina.RegainStamina(20);
                }
            }
            else
            {
                animator.SetTrigger(triggerAttackWeak);
                playerStamina.ReduceStamina(10);
            }

        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            if (distanceToEnemy <= attackRange)
            {
                if (playerStamina.currentStamina > 0)
                {
                    animator.SetTrigger(triggerAttackWeak);
                    // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                    if (!boss1NormalManagement.baseBoss.CanDodge(10 + attackDamage, 6))
                    {
                        // Kẻ địch không né được, tiến hành tấn công
                        dame.SetActive(true);
                        attackSound.Play();
                        int damageDealt = Mathf.Max(10 + attackDamage - boss1NormalManagement.baseBoss.defense, 0); // Trừ phòng thủ của địch
                        boss1NormalManagement.TakeDamage(damageDealt);
                        dameText.text = $"{damageDealt}";
                    }
                    else
                    {
						otherAnimator.SetTrigger(triggerDodged);
                        dodgeSound.Play();
                        dodge.SetActive(true);
                        dodgeText.text = "Dodged!";
                    }
                    StartCoroutine(HideGuideTextAfterDelay(1f));
                    // Trừ stamina sau khi tấn công
                    playerStamina.ReduceStamina(10);
                }
                else
                {
                    Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                    playerStamina.RegainStamina(20);
                }
            }
            else
            {
                animator.SetTrigger(triggerAttackWeak);
                playerStamina.ReduceStamina(10);
            }

        }
        else if (this.gameObject.scene.name == "Boss2")
        {
            if (distanceToEnemy <= attackRange)
            {
                if (playerStamina.currentStamina > 0)
                {
                    animator.SetTrigger(triggerAttackWeak);
                    // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                    if (!boss1NormalManagement.baseBoss.CanDodge(10 + attackDamage, 6))
                    {
                        // Kẻ địch không né được, tiến hành tấn công
                        dame.SetActive(true);
                        attackSound.Play();
                        int damageDealt = Mathf.Max(10 + attackDamage - boss1NormalManagement.baseBoss.defense, 0); // Trừ phòng thủ của địch
                        boss1NormalManagement.TakeDamage(10 + damageDealt);
                        dameText.text = $"{10 + damageDealt}";
                    }
                    else
                    {
						otherAnimator.SetTrigger(triggerDodged);
                        dodgeSound.Play();
                        dodge.SetActive(true);
                        dodgeText.text = "Dodged!";
                    }
                    StartCoroutine(HideGuideTextAfterDelay(1f));
                    // Trừ stamina sau khi tấn công
                    playerStamina.ReduceStamina(10);
                }
                else
                {
                    Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                    playerStamina.RegainStamina(20);
                }
            }
            else
            {
                animator.SetTrigger(triggerAttackWeak);
                playerStamina.ReduceStamina(10);
            }

        }

    }

    public void AttackNormal()
    {
        if (this.gameObject.scene.name == "TurtorialMap")
        {
            if (enemyHealth != null && playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackNormal);
                // Nếu đứng gần đủ, tính toán né tránh của kẻ địch
                if (!enemyHealth.CanDodge())
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(15+attackDamage - enemyHealth.defense, 0); // Trừ phòng thủ của địch
                    enemyHealth.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                // Trừ stamina sau khi tấn công
                playerStamina.ReduceStamina(15);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "NormalMap")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackNormal);
                if (!enemyStats.CanDodge(15+attackDamage, 8))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(15+attackDamage - enemyNormalManagement.stats.defense, 0);
                    enemyNormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(15);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss1")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackNormal);
                if (!boss1NormalManagement.baseBoss.CanDodge(15+attackDamage, 8))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(15+attackDamage - boss1NormalManagement.baseBoss.defense, 0);
                    boss1NormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(15);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackNormal);
                if (!boss1NormalManagement.baseBoss.CanDodge(15+attackDamage, 8))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(15+attackDamage - boss1NormalManagement.baseBoss.defense, 0);
                    boss1NormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(15);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss2")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackNormal);
                if (!boss1NormalManagement.baseBoss.CanDodge(15 + attackDamage, 8))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(15 + attackDamage - boss1NormalManagement.baseBoss.defense, 0);
                    boss1NormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(15);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
    }

    // Hàm tấn công mạnh
    public void AttackStrong()
    {
        if (this.gameObject.scene.name == "TurtorialMap")
        {
            if (enemyHealth != null && playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackStrong);
                if (!enemyHealth.CanDodge())
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(20+ attackDamage - enemyHealth.defense, 0);
                    enemyHealth.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(20);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "NormalMap")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackStrong);
                if (!enemyStats.CanDodge(20+attackDamage, 10))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(20+attackDamage - enemyNormalManagement.stats.defense, 0);
                    enemyNormalManagement.TakeDamage( damageDealt);
                    dameText.text = $"{ damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(20);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss1")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackStrong);
                if (!boss1NormalManagement.baseBoss.CanDodge(20+attackDamage, 10))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(20 + attackDamage - boss1NormalManagement.baseBoss.defense, 0);
                    boss1NormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{ damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(20);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss3")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackStrong);
                if (!boss1NormalManagement.baseBoss.CanDodge(20 + attackDamage, 10))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(20 + attackDamage - boss1NormalManagement.baseBoss.defense, 0);
                    boss1NormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(20);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
        else if (this.gameObject.scene.name == "Boss2")
        {
            if (playerStamina.currentStamina > 0)
            {
                animator.SetTrigger(triggerAttackStrong);
                if (!boss1NormalManagement.baseBoss.CanDodge(20 + attackDamage, 10))
                {
                    dame.SetActive(true);
                    attackSound.Play();
                    int damageDealt = Mathf.Max(20 + attackDamage - boss1NormalManagement.baseBoss.defense, 0);
                    boss1NormalManagement.TakeDamage(damageDealt);
                    dameText.text = $"{damageDealt}";
                }
                else
                {
					otherAnimator.SetTrigger(triggerDodged);
                    dodgeSound.Play();
                    dodge.SetActive(true);
                    dodgeText.text = "Dodged!";
                }
                StartCoroutine(HideGuideTextAfterDelay(1f));
                playerStamina.ReduceStamina(20);
            }
            else
            {
                Debug.Log("Không đủ stamina, tự động nghỉ ngơi");
                playerStamina.RegainStamina(20);
            }
        }
    }

    // Hàm load dữ liệu của nhân vật từ file JSON
    private void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Tải tất cả chỉ số từ file
            attackDamage = 10 + data.attack;

            Debug.Log("Dữ liệu nhân vật đã được load thành công!");
        }
        else
        {
            Debug.LogError("Không tìm thấy file lưu trữ tại: " + saveFilePath);
        }
    }

    // Coroutine để ẩn guideText sau một khoảng thời gian
    private IEnumerator HideGuideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dameText.text = ""; // Ẩn text bằng cách xóa nội dung của nó
        dodgeText.text = "";
        dame.SetActive(false);
        dodge.SetActive(false);
    }

    // Cấu trúc dữ liệu để lưu trữ và load chỉ số
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int attack;
        public int defense;
        public int hp;
        public int stamina;
        public int dodge;
        public int skillPoints;
    }
}