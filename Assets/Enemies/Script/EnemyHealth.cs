using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Animator animator;

    public int max_health = 100;
    int current_health = 0;

    void Start()
    {
        current_health = max_health;
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (current_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Play sound
        
        //Play Death Animationo of enemy
        animator.SetBool("IsDeath", true);

        //Disable the EnemyAI script and box collider
        EnemyAl enemyAl = FindObjectOfType<EnemyAl>();
        enemyAl.enabled = false;

        Destroy(this.gameObject, 1f);
    }

    public void TakeDamage(int damage)
    {
        current_health -= damage;
        animator.SetTrigger("IsHurt");
    }
}
