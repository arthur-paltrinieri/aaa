using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public NavMeshAgent hunter; // Refer�ncia ao componente NavMeshAgent do inimigo
    public Transform player; // Refer�ncia ao objeto do jogador
    public LayerMask whatIsGround, whatIsPlayer; // M�scaras de camada para identificar o terreno e o jogador
    public Animator animator; // Adiciona uma refer�ncia ao componente Animator
    //patrulhando
    public Vector3 walkPoint; // Ponto para onde o inimigo est� caminhando
    bool walkPointSet; // Verifica se um ponto de caminhada foi definido
    public float walkPoitRange; // Raio dentro do qual o inimigo pode definir um ponto de caminhada
    public Transform projectileLaunchPoint; // Mostra de onde o projetil vai ser disparado
    //ataque
    public float timeBetewwnAttacks; // Tempo entre os ataques
    bool alreadyAttacked; // Verifica se o inimigo j� atacou
    public GameObject projectile; // Prefab do proj�til

    //states
    public float sightRange, attackRange; // Alcance de vis�o e alcance de ataque do inimigo
    public bool playerInSightRange, playerInAttackRange; // Verifica se o jogador est� no alcance de vis�o ou alcance de ataque
    public float followDistance; // Dist�ncia de persegui��o do jogador
    public void Start()
    {
        // Encontra o objeto do jogador e inicializa o NavMeshAgent do inimigo
        player = GameObject.Find("Play er").transform;
        hunter = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();// Obt�m o componente Animator
    }
   

    private void Update()
    {
        if (playerInSightRange)
        {
            // Se o jogador estiver � vista, verifica se h� obst�culos entre o inimigo e o jogador
            if (playerInSightRange)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, sightRange, whatIsGround))
                {
                    // Se um obst�culo estiver no caminho, o jogador n�o est� � vista
                    if (hit.collider.CompareTag("wall"))
                    {
                        playerInSightRange = false;
                        return;
                    }
                }
            }
        }

        // Verifica se o jogador est� dentro do alcance de vis�o ou alcance de ataque
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Determina o comportamento do inimigo com base na posi��o do jogador
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
        // Se um ponto de caminhada n�o estiver definido, busca um
        if (!walkPointSet) { SearchWalkPoint(); }

        // Define o destino do NavMeshAgent para o ponto de caminhada
        if (walkPointSet)
        {
            hunter.SetDestination(walkPoint);
        }

        // Verifica se o inimigo alcan�ou o ponto de caminhada
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

        // Verifica se o ponto de caminhada est� no terreno
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        // Persegue o jogador se estiver dentro da dist�ncia de persegui��o
        if (Vector3.Distance(transform.position, player.position) < followDistance)
        {
            hunter.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        // Olha na dire��o do jogador e ataca se n�o tiver atacado recentemente
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
