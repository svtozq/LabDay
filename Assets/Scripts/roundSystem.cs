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
    [SerializeField] private TextMeshProUGUI waveText1;
    [SerializeField] private TextMeshProUGUI waveCooldownText;
    [SerializeField] private TextMeshProUGUI waveCooldownText1;
    [SerializeField] private Material skyMaterial;
    [SerializeField] private Gradient skyWhiteToRed;
    [SerializeField] private Gradient skyRedToWhite;
    [SerializeField] private Color colorStart;
    
    private float colorChangeSpeed = 0.175f;
    private float cooldown;
    private int waveNumber = 0;
    private float timeBtwWaves = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 10f;
        skyMaterial.SetColor("_Tint", colorStart);
    }

    // Update is called once per frame
    void Update()
    {
        waveCooldownText.enabled = false;
        waveCooldownText1.enabled = false;
        
        if (GameObject.FindGameObjectsWithTag("Ennemy").Length <= 0 && waveNumber < 2)
        {
            waveCooldownText.enabled = true;
            waveCooldownText1.enabled = true;
            cooldown -= Time.deltaTime;

            if (cooldown <= 5.5f && cooldown > 5.4f)
            {
                StartCoroutine(ChangeSkyColor1());
            }

            if (cooldown <= 19.5f && cooldown > 19.4f)
            {
                StartCoroutine(ChangeSkyColor2());
            }

            if (cooldown <= 0f)
            {
                StartCoroutine(SpawnWave());
                cooldown = timeBtwWaves;
            }
        }

        else if (GameObject.FindGameObjectsWithTag("Ennemy").Length <= 0 && waveNumber >= 2)
        {
            StartCoroutine(ChangeSkyColor2());
            waveText.enabled = false;
            waveText1.enabled = false;
            return;
        }
        
        waveText.text = Mathf.Floor(waveNumber).ToString();
        waveCooldownText.text = Mathf.Floor(cooldown).ToString();
        Debug.Log(waveNumber);
    }

    IEnumerator ChangeSkyColor1()
    {
        float t = 0f; // Time tracker

        while (t < 1f)
        {
            t += Time.deltaTime * colorChangeSpeed;
            skyMaterial.SetColor("_Tint", skyWhiteToRed.Evaluate(t));
            yield return null; // Wait for next frame
        }
    }
    
    IEnumerator ChangeSkyColor2()
    {
        float t = 0f; // Time tracker

        while (t < 1f)
        {
            t += Time.deltaTime * colorChangeSpeed;
            skyMaterial.SetColor("_Tint", skyRedToWhite.Evaluate(t));
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
