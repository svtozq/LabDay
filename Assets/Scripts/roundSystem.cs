using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roundSystem : MonoBehaviour
{
    
    [SerializeField] private Transform zombie;
    [SerializeField] private float timeBtwWaves = 5f;
    
    private float countdown = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        
        if (countdown <= 0f)
        {
            SpawnWave();
            countdown = timeBtwWaves;
        }
    }

    void SpawnWave()
    {
        Debug.Log("Spawning wave");
        
    }
}
