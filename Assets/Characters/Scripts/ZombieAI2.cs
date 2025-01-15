using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Cible à suivre")]
    public Transform target; // La cible que le zombie doit poursuivre

    private NavMeshAgent agent; // Le composant NavMeshAgent du zombie

    void Start()
    {
        // Récupérer le composant NavMeshAgent
        agent = GetComponent<NavMeshAgent>();

        // Vérifications initiales
        if (agent == null)
        {
            Debug.LogError("Erreur : Le composant NavMeshAgent est manquant sur " + gameObject.name);
            enabled = false; // Désactiver le script si le NavMeshAgent est absent
            return;
        }

        if (target == null)
        {
            Debug.LogWarning("Aucune cible assignée au zombie. Assigne une cible dans l'inspecteur.");
        }
    }

    void Update()
    {
        // Si une cible est assignée et que le NavMeshAgent est valide
        if (target != null && agent != null)
        {
            // Déplace le zombie vers la cible
            agent.SetDestination(target.position);

            // Vérifier si le zombie est proche de la cible
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Debug.Log("Le zombie a atteint la cible !");
            }
        }
        else
        {
            // Message de débogage si la cible est manquante
            if (target == null)
            {
                Debug.LogWarning("La cible est toujours manquante !");
            }
        }
    }
}
