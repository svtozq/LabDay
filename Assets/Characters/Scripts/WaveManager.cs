using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Références")]
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    [Header("Paramètres de la vague")]
    public int startZombieCount = 5; // Nombre initial de zombies
    public float healthMultiplier = 1.2f; // Multiplicateur de vie à chaque manche
    public float speedMultiplier = 1.1f; // Multiplicateur de vitesse à chaque manche
    public float waveInterval = 5f; // Temps entre chaque vague

    private int currentRound = 1;
    private int remainingZombies;

    void Start()
    {
        StartCoroutine(StartNextWave()); // Démarre la première vague
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        
        int zombieCount = startZombieCount + (currentRound * 2); // Augmente le nombre de zombies
        remainingZombies = zombieCount;

        for (int i = 0; i < zombieCount; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(0.5f); // Laisse un délai entre chaque spawn
        }

        Debug.Log("🌊 Manche " + currentRound + " commencée avec " + zombieCount + " zombies !");
    }

    void SpawnZombie()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        ZombieAI zombieScript = zombie.GetComponent<ZombieAI>();
        zombieScript.SetStats(healthMultiplier * currentRound, speedMultiplier * currentRound); // Augmente la difficulté

        zombieScript.OnDeath += ZombieKilled; // Écouteur pour détecter la mort d’un zombie
    }

    void ZombieKilled()
    {
        remainingZombies--;

        if (remainingZombies <= 0)
        {
            Debug.Log("✔️ Manche " + currentRound + " terminée !");
            currentRound++;
            StartCoroutine(StartNextWave()); // Démarre la prochaine vague
        }
    }
}
