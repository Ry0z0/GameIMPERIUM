using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorialAIController : MonoBehaviour
{
    private EnemyTutorialMovement enemyMovement;
    private EnemyTutorialAttack enemyAttack;
    private EnemyTutorialStamina enemyStamina;
    private Transform playerTransform;
    private Transform enemyTransform;
    public float attackRange = 1f;
    public float safeDistance = 1f;
    void Start()
    {
        enemyMovement = GetComponent<EnemyTutorialMovement>();
        enemyAttack = GetComponent<EnemyTutorialAttack>(); // Lấy EnemyAttack
        enemyStamina = GetComponent<EnemyTutorialStamina>(); // Lấy EnemyStamina
        playerTransform = GameObject.FindWithTag("Player").transform;
        enemyTransform = GameObject.FindWithTag("enemy").transform;

    }
    public void MakeDecision()
    {
        if (enemyStamina.currentStamina < 10)
        {
            enemyMovement.Rest();
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(enemyTransform.transform.position, playerTransform.position);
            // Nếu enemy đang ở xa player, tiến về phía player
            if (distanceToPlayer > attackRange)
            {
                if (distanceToPlayer > safeDistance)
                {
                    enemyMovement.MoveLeft(); // Di chuyển về phía player
                }
            }
            else
            {
                enemyAttack.Attack();
            }
        }
    }
}
