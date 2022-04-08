using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour

{
    private HealthBar healt;
    public Animator animator;
    public Transform attackPoint;
    public LayerMask Layers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
       healt = FindObjectOfType<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("attack");
        }
    }
    public void OnEnemyAttack()
    {
        healt.maxHealth -= attackDamage;
    }
}
