using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform target; // La cible (par ex., le joueur)
    private NavMeshAgent agent; // Composant pour le déplacement

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Récupère l'agent NavMesh
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position); // Déplace le zombie vers la cible
        }
    }
}
