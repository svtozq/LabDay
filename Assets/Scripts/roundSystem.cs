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
    [SerializeField] private TextMeshProUGUI waveCooldownText1;
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Gradient skyGradient;
    private float colorChangeSpeed = 0.17f;
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
        waveCooldownText.enabled = false;
        waveCooldownText1.enabled = false;
        
        if (GameObject.FindGameObjectsWithTag("Ennemy").Length <= 0 )
        {
            waveCooldownText.enabled = true;
            waveCooldownText1.enabled = true;
            cooldown -= Time.deltaTime;
            StartCoroutine(ChangeSkyColor());
            
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

    IEnumerator ChangeSkyColor()
    {
        float t = 0f; // Time tracker

        while (t < 1f)
        {
            t += Time.deltaTime * colorChangeSpeed;
            skyMaterial.SetColor("_Tint", skyGradient.Evaluate(t));
            yield return null; // Wait for next frame
        }
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
