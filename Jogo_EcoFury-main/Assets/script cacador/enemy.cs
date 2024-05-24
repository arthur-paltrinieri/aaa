using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent hunter; // Referência ao componente NavMeshAgent do inimigo
    public Transform player; // Referência ao objeto do jogador
    public LayerMask whatIsGround, whatIsPlayer; // Máscaras de camada para identificar o terreno e o jogador
    public Animator animator; // Adiciona uma referência ao componente Animator
    //patrulhando
    public Vector3 walkPoint; // Ponto para onde o inimigo está caminhando
    bool walkPointSet; // Verifica se um ponto de caminhada foi definido
    public float walkPoitRange; // Raio dentro do qual o inimigo pode definir um ponto de caminhada
    public Transform projectileLaunchPoint; // Mostra de onde o projetil vai ser disparado
    //ataque
    public float timeBetewwnAttacks; // Tempo entre os ataques
    bool alreadyAttacked; // Verifica se o inimigo já atacou
    public GameObject projectile; // Prefab do projétil

    //states
    public float sightRange, attackRange; // Alcance de visão e alcance de ataque do inimigo
    public bool playerInSightRange, playerInAttackRange; // Verifica se o jogador está no alcance de visão ou alcance de ataque
    public float followDistance; // Distância de perseguição do jogador
    public void Start()
    {
        // Encontra o objeto do jogador e inicializa o NavMeshAgent do inimigo
        player = GameObject.Find("Play er").transform;
        hunter = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();// Obtém o componente Animator
    }
   

    private void Update()
    {
        if (playerInSightRange)
        {
            // Se o jogador estiver à vista, verifica se há obstáculos entre o inimigo e o jogador
            if (playerInSightRange)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange, whatIsGround))
                {
                    // Se um obstáculo estiver no caminho, o jogador não está à vista
                    if (hit.collider.CompareTag("wall"))
                    {
                        playerInSightRange = false;
                        return;
                    }
                }
            }
        }

        // Verifica se o jogador está dentro do alcance de visão ou alcance de ataque
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Determina o comportamento do inimigo com base na posição do jogador
        if (!playerInSightRange && !playerInAttackRange)
        {
            
            ChasePlayer();
        }
        else if (playerInAttackRange)
        {
           
            AttackPlayer();
        }
        else
        {
            
            Patroling();
        }
    }

    private void Patroling()
    {
        // Se um ponto de caminhada não estiver definido, busca um
        if (!walkPointSet) { SearchWalkPoint(); }

        // Define o destino do NavMeshAgent para o ponto de caminhada
        if (walkPointSet)
        {
            hunter.SetDestination(walkPoint);
        }

        // Verifica se o inimigo alcançou o ponto de caminhada
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (Vector3.Distance(transform.position, walkPoint) < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        // Gera aleatoriamente um ponto de caminhada dentro de um raio
        float randomZ = Random.Range(-walkPoitRange, walkPoitRange);
        float randomX = Random.Range(-walkPoitRange, walkPoitRange);
        walkPoint = new Vector3(transform.position.x, transform.position.y, randomZ);

        // Verifica se o ponto de caminhada está no terreno
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Persegue o jogador se estiver dentro da distância de perseguição
        if (Vector3.Distance(transform.position, player.position) < followDistance)
        {
            hunter.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        // Olha na direção do jogador e ataca se não tiver atacado recentemente
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            Vector3 launchPoint = projectileLaunchPoint.position;
            Rigidbody rb = Instantiate(projectile, launchPoint, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetewwnAttacks);
        }
        else { return; }
    }

    private void ResetAttack()
    {
        // Reseta a flag de ataque
        alreadyAttacked = false;
    }
}
