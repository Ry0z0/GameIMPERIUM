using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AIController : MonoBehaviour
{
    private Boss1NormalManagement boss1NormalManagement;
    private BossMovement bossMovement;
    private PowerBoss1 powerBoss1;
    private Transform playerTransform;
    public float attackRange = 2.0f;



    private void Start()
    {
        boss1NormalManagement = GetComponent<Boss1NormalManagement>();
        bossMovement = GetComponent<BossMovement>();
        powerBoss1 = GetComponent<PowerBoss1>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
    public void MakeDecision()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (boss1NormalManagement.baseBoss.stamina <= 0)
        {
            Debug.Log("Tao nghỉ ngơi nè");
            boss1NormalManagement.Rest();
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
            bossMovement.MoveLeft();
        }
    }

    private void PerformAttack()
    {
        // Lựa chọn kiểu tấn công dựa trên chỉ số của enemy
        if (boss1NormalManagement.baseBoss.stamina >= 20) // Tấn công mạnh nếu đủ stamina
        {
            powerBoss1.AttackStrong();
        }
        else
        {
            powerBoss1.AttackNormal();
        }
    }
}
