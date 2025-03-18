using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class roundSystem : MonoBehaviour
{
    
    [SerializeField] private Transform zombie;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI waveCooldownText;
    private float cooldown = 5.5f;
    private int waveNumber = 0;
    private float timeBtwWaves = 5.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectsWithTag("Ennemy").Length <= 0)
        {
            cooldown -= Time.deltaTime;
            
            if (cooldown <= 0f)
            {
                StartCoroutine(SpawnWave());
                cooldown = timeBtwWaves;
            }
        }
        
        waveText.text = Mathf.Floor(waveNumber).ToString();
        waveCooldownText.text = Mathf.Round(cooldown).ToString();
        Debug.Log(waveNumber);
    }

    IEnumerator SpawnWave()
    {
        waveNumber++;
        
        for (int i = 0; i < waveNumber * 10 ; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;
        
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnLocation = spawnPoints[randomIndex];
        
        Instantiate(zombie, spawnLocation.position, spawnLocation.rotation);
    }
}
