using UnityEngine;
using UnityEngine.AI;
using System;

public class ZombieAI : MonoBehaviour
{
    [Header("Composants")]
    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    [Header("Données")]
    public float health = 100f;

    [Header("Détection")]
    public LayerMask whatIsGround, whatIsPlayer;
    public float sightRange = 200f, attackRange = 20f;
    private bool playerInSightRange, playerInAttackRange;

    [Header("Patrouille")]
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    [Header("Attaque")]
    public float attackCooldown = 1.5f;
    private bool alreadyAttacked;
    public int attackDamage = 20;

    public event Action OnDeath;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
        else
            Debug.LogError("⚠️ Le joueur n'a pas été trouvé ! Vérifie son tag.");
    }

    private void Start()
    {
        if (agent == null)
        {
            Debug.LogError("❌ NavMeshAgent manquant sur " + gameObject.name);
        }
        else
        {
            agent.isStopped = false;
            agent.speed = 90f;
            Debug.Log("✅ NavMeshAgent initialisé avec succès !");
        }
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        Debug.Log($"📍 Player détecté: {playerInSightRange}, Player en attaque: {playerInAttackRange}");

        if (!playerInSightRange && !playerInAttackRange)
        {
            Debug.Log("🚶‍♂️ Zombie en patrouille...");
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            Debug.Log("🏃‍♂️ Zombie poursuit le joueur !");
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            Debug.Log("💥 Zombie attaque le joueur !");
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Debug.Log($"🧭 Zombie se dirige vers un point de patrouille: {walkPoint}");
        }
        
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f) 
        {
            walkPointSet = false;
            Debug.Log("🔄 Nouveau point de patrouille nécessaire !");
        }

        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            Debug.Log($"🔍 Nouveau point de patrouille trouvé: {walkPoint}");
        }
    }

    private void ChasePlayer()
    {
        if (agent != null && player != null)
        {
            Debug.Log($"🚀 Zombie se dirige vers le joueur à la position {player.position}");
            agent.isStopped = false;
            agent.SetDestination(player.position);

            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }
    }

    private void AttackPlayer()
    {
        agent.isStopped = true;
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetBool("isAttacking", true);
            Debug.Log("💥 Le zombie attaque le joueur !");
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        agent.isStopped = false;
        Debug.Log("🔄 Le zombie peut attaquer à nouveau !");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"🩸 Le zombie a pris {damage} dégâts, vie restante: {health}");

        if (health <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("☠️ Zombie éliminé !");
        animator.SetTrigger("Die");
        agent.isStopped = true;
        OnDeath?.Invoke();
        Destroy(gameObject, 2f);
    }
}
