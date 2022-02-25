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

    public void TakeDamage(int damage)
    {
        max_health -= damage;
    }
}
