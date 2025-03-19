using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class characterHealth : MonoBehaviour
{
    public float health;
    public float  maxHealth = 100f;
    
    public Image healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = health / maxHealth;

        if (health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Character has died!");
        Destroy(gameObject);
    }
}
