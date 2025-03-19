using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class characterHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100f;
    private float cooldownDie;
    public Image healthBar;
    public string thisScene;
    [SerializeField] private GameObject defeatCanva;
    
    // Start is called before the first frame update
    void Awake()
    { 
        Time.timeScale = 1f;
    }
    void Start()
    {
        health = maxHealth;
        cooldownDie = 3f;
        defeatCanva.SetActive(false);
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
            StartCoroutine(Die());
        }
    }
    
    IEnumerator Die()
    {
        Debug.Log("Character has died!");
        float deathTime = 0f;
        Time.timeScale = 0f;
        defeatCanva.SetActive(true);

        while (deathTime < cooldownDie)
        {
            deathTime += Time.unscaledDeltaTime;
            yield return null;
        }
        
        SceneManager.LoadScene(thisScene);
    }
}
