using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private  Patroler patroler;

    public Rigidbody2D rb;
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemy died!");
        patroler = GetComponent<Patroler>();
        patroler.enabled = false;

        animator.SetBool("isDead", true);
        rb.bodyType = RigidbodyType2D.Static;

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Invoke("Kill", 5f);
       

    }
    void Kill()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
