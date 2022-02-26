using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int max_health = 100;
    int current_health = 0;

    void Start()
    {
        current_health = max_health;
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

        //play Particle Effect

        //Disable the EnemyAI script and box collider
        EnemyAl enemyAl = FindObjectOfType<EnemyAl>();
        enemyAl.GetComponent<BoxCollider2D>().enabled = false;
        enemyAl.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        current_health -= damage;
    }
}
