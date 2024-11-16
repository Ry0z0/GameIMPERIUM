using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalAIController : MonoBehaviour
{
    private EnemyNormalManagement enemyManagement;
    private EnemyMovement enemyMovement;
    private EnemyNormalAttack enemyNormalAttack;
    private Transform playerTransform;
    public float attackRange = 2.0f;



    private void Start()
    {
        enemyManagement = GetComponent<EnemyNormalManagement>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyNormalAttack = GetComponent<EnemyNormalAttack>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    public void MakeDecision()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position); 
        if (enemyManagement.stats.stamina <= 0)
        {
            Debug.Log("Tao nghỉ ngơi nè");
            enemyManagement.Rest();
            return; 
        }
        if (distanceToPlayer <= attackRange)
        {
            Debug.Log("Tao đánh nè");
            PerformAttack();
        }
        else
        {
            Debug.Log("Tao đi đây");
            enemyMovement.MoveLeft();
        }
    }

    private void PerformAttack()
    {
        // Lựa chọn kiểu tấn công dựa trên chỉ số của enemy
        if (enemyManagement.stats.stamina >= 20) // Tấn công mạnh nếu đủ stamina
        {
            enemyNormalAttack.AttackStrong();
        }
        else 
        {
            enemyNormalAttack.AttackNormal();
        }
    }
}
