using UnityEngine;
using UnityEngine.AI;
using System;

public class ZombieAI : MonoBehaviour
{
    // 🎯 [Header] Catégorisation des variables dans l'inspecteur Unity
    [Header("Composants")]
    private NavMeshAgent agent; // Composant permettant au zombie de se déplacer sur le terrain
    private Animator animator;  // Gère les animations du zombie
    private Transform player;   // Stocke la position du joueur

    [Header("Données")]
    public float health = 100f; // Vie du zombie

    [Header("Détection")]
    public LayerMask whatIsGround, whatIsPlayer; // Couches pour détecter le sol et le joueur
    public float sightRange = 120f, attackRange = 15f; // Distance de vision et d'attaque
    private bool playerInSightRange, playerInAttackRange; // Indique si le joueur est dans la zone de vision ou d'attaque

    [Header("Patrouille")]
    private Vector3 walkPoint; // Position cible lors de la patrouille
    private bool walkPointSet; // Vérifie si un point de patrouille a été défini
    public float walkPointRange; // Rayon dans lequel le zombie cherche un point de patrouille

    [Header("Attaque")]
    public float attackCooldown = 1.5f; // Temps d'attente entre deux attaques
    private bool alreadyAttacked; // Vérifie si le zombie a déjà attaqué récemment
    public int attackDamage = 10; // Dégâts infligés par le zombie

    public event Action OnDeath; // Événement pour signaler au WaveManager qu'un zombie est mort

    private void Awake()
    {
        // 🔄 Récupère les composants attachés au zombie
        agent = GetComponent<NavMeshAgent>(); // Récupère le composant de navigation
        animator = GetComponent<Animator>();  // Récupère le gestionnaire d'animations

        // 🔎 Trouve le joueur dans la scène
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform; // Stocke la position du joueur
        else
            Debug.LogError("⚠️ Le joueur n'a pas été trouvé ! Vérifie son tag.");
    }

    private void Update()
    {
        // 🧐 Vérifie si le joueur est visible ou à portée d'attaque
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // 🚶 Si le joueur n'est pas visible, patrouille
        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        // 🏃 Si le joueur est visible mais hors d'attaque, poursuite
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        // ⚔ Si le joueur est à portée d'attaque, attaque !
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patrolling()
    {
        // 👀 Si aucun point de patrouille n'est défini, en chercher un
        if (!walkPointSet) SearchWalkPoint();

        // 🏃 Si un point est défini, s'y rendre
        if (walkPointSet) agent.SetDestination(walkPoint);

        // 📏 Mesure la distance entre le zombie et son objectif
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // ✅ Si le zombie est arrivé à destination, chercher un nouveau point
        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;

        // 🎭 Active l'animation de marche
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
    }

    private void SearchWalkPoint()
    {
        // 🎲 Génère un point aléatoire dans un certain rayon autour du zombie
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // 🛑 Vérifie que le point est bien sur le sol avant de l'accepter
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // 🏃 Si le zombie et le joueur existent bien, poursuivre le joueur
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position); // Oriente le zombie vers le joueur

            // 🔄 Change l'animation en mode "course"
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }
    }

    private void AttackPlayer()
    {
        // 🛑 Stoppe le mouvement du zombie quand il attaque
        agent.SetDestination(transform.position);

        // 👀 Regarde le joueur pour attaquer
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // ⚔ Joue l'animation d'attaque
            animator.SetBool("isAttacking", true);
            Debug.Log("💥 Le zombie attaque le joueur !");

			characterHealth playerHealth = player.GetComponent<characterHealth>();
        	if (playerHealth != null)
        	{
            	playerHealth.TakeDamage(attackDamage); // Deal damage
        	}
            
            alreadyAttacked = true; // Empêche l'attaque multiple immédiate
            Invoke(nameof(ResetAttack), attackCooldown); // Relance l'attaque après un certain temps
        }
    }

    private void ResetAttack()
    {
        // 🔄 Permet au zombie d'attaquer à nouveau après un délai
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        // 💔 Réduit la vie du zombie lorsqu'il prend des dégâts
        health -= damage;
        
        // ☠️ Si la vie tombe à 0, le zombie meurt
        if (health <= 0) Die();
    }

    public void SetStats(float healthMultiplier, float speedMultiplier)
    {
        // 💪 Augmente la vie du zombie selon un multiplicateur
        health *= healthMultiplier;
        
        // 🏃 Augmente la vitesse du zombie selon un multiplicateur
        agent.speed *= speedMultiplier;
    }

    private void Die()
    {
        // ☠ Affiche un message dans la console quand le zombie meurt
        Debug.Log("☠️ Zombie éliminé !");
        
        // 🎭 Lance l'animation de mort
        animator.SetTrigger("Die");
        
        // ⛔ Empêche le zombie de bouger après sa mort
        agent.isStopped = true;
        
        // 📢 Informe le WaveManager que ce zombie est mort
        OnDeath?.Invoke();

        // 🗑️ Détruit l'objet zombie après 2 secondes
        Destroy(gameObject, 2f);
    }
}
