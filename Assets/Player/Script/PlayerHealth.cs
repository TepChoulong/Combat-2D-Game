using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

        //Play Death Animation

        //Play Particle effect

        //Disable the controller scritp and box collider
        PlayerController playerController = FindObjectOfType<PlayerController>();
        playerController.enabled = false;
        playerController.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void TakeDamage(int damage)
    {
        current_health -= damage;
    }
}
