using UnityEngine;
using UnityEngine.AI;

public class ZombieAI2 : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Audio
    public AudioSource zombieAudioSource; // Composant AudioSource
    public AudioClip zombieGroan; // Son du zombie
    public float soundRange = 10f; // Distance pour jouer le son

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        zombieAudioSource = GetComponent<AudioSource>();

        if (player == null)
            Debug.LogError("Player non trouvé ! Vérifie que l'objet s'appelle bien 'PlayerObj'.");

        if (agent == null)
            Debug.LogError("NavMeshAgent non trouvé ! Vérifie que le composant est attaché au zombie.");

        if (zombieAudioSource == null)
            Debug.LogError("AudioSource non trouvé ! Ajoute un AudioSource au zombie.");
    }

    private void Update()
    {
        // Vérifie les distances de détection du joueur
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        Debug.Log("Player in Sight: " + playerInSightRange + ", Player in Attack: " + playerInAttackRange);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

        // Joue un son si le joueur est proche
        if (Vector3.Distance(transform.position, player.position) < soundRange)
        {
            PlayZombieSound();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Debug.Log("Zombie patrouille vers : " + walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            Debug.Log("Point de patrouille atteint, recherche d'un nouveau point...");
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log("Nouveau point de patrouille trouvé : " + walkPoint);
        }
    }

    private void ChasePlayer()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
            Debug.Log("Zombie poursuit le joueur...");
        }
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        Debug.Log("Zombie attaque le joueur !");

        if (!alreadyAttacked)
        {
            if (projectile != null)
            {
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                Debug.Log("Projectile tiré !");
            }
            else
            {
                Debug.LogError("Projectile non assigné ! Vérifie que l'objet est bien attaché dans l'Inspector.");
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        Debug.Log("Zombie prêt à attaquer à nouveau.");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Zombie a pris " + damage + " de dégâts. Vie restante : " + health);

        if (health <= 0)
        {
            Debug.Log("Zombie éliminé !");
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void PlayZombieSound()
    {
        if (!zombieAudioSource.isPlaying && zombieGroan != null)
        {
            zombieAudioSource.PlayOneShot(zombieGroan);
            Debug.Log("Le zombie grogne !");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
