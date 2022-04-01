using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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
            animator.SetBool("IsDeath", true);
        }
    }

    void Die()
    {
        //Play sound
        
        //Disable the controller scritp and box collider
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.enabled = false;

        EnemyAl enemyAl = FindObjectOfType<EnemyAl>();
        enemyAl.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        current_health -= damage;
        animator.SetTrigger("IsHurt");
    }
}
