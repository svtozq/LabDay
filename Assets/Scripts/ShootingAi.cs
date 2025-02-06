using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAi : MonoBehaviour
{
    public int health = 100; // Enemy health

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Remove enemy from the scene
    }
}
