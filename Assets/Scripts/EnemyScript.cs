using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{

    private GameObject enemySprite;

    public NavMeshAgent agent;
    private float tempoDeChegada;
    private bool chegou = false;
    [SerializeField] private float duracaoDaRonda = 7f; //Segundos
    [SerializeField] private float margemDistanciaDestino = 0.1f;
    public bool rondando = true;

    public bool nocauteado = false;
    public bool recuperando = false;
    [SerializeField] private float tempoDeRecuperacao = 10f;
    private float tempoDeNocaute;

    public float radius = 5f;
    [Range(0, 360)]
    public float angle = 45f;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    public bool perseguindo = false;
    [SerializeField] private float margemDistanciaAtaque = 0.5f;
    [SerializeField] private float enemyAttackCooldown = 1.5f;
    private float enemyLastAttackTime = 0f;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        enemySprite = GameObject.FindGameObjectsWithTag("EnemySprite")[0];

        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        checarDirecao();

        if (canSeePlayer)
        {
            rondando = false;
            perseguindo = true;
        }
        else
        {
            rondando = true;
            perseguindo = false;
        }

        if(perseguindo)
        {
            perseguirJogador();
            alcancarJogador();
        }

        if(rondando)
        {
            if(nocauteado)
            {
                nocaute();
            }
            else
            {
                rondar();
            }

        }
    }

    private void alcancarJogador()
    {
        if (agent.remainingDistance <= agent.stoppingDistance + margemDistanciaAtaque)
        {

            enemySprite.GetComponent<Animator>().SetBool("walking", false);
            chegou = true;
            atacarJogador();
        }
    }

    private void atacarJogador()
    {
        if (Time.time > (enemyLastAttackTime + enemyAttackCooldown))
        {
            enemySprite.GetComponent<Animator>().SetBool("attacking", true);
            playerRef.GetComponent<PlayerInventoryScript>().playerHP -= 1;
            enemyLastAttackTime = Time.time;
        }
        
        enemySprite.GetComponent<Animator>().SetBool("attacking", false);
    }

    private void perseguirJogador()
    {
        agent.SetDestination(playerRef.transform.position);
        enemySprite.GetComponent<Animator>().SetBool("walking", true);
    }

    void nocaute()
    {
        agent.isStopped = true;
        enemySprite.GetComponent<Animator>().SetBool("walking", false);

        if (!recuperando)
        {
            recuperando = true;
            tempoDeNocaute = Time.time;

        } else
        {
            if(Time.time > (tempoDeNocaute + tempoDeRecuperacao))
            {
                nocauteado = false;
                recuperando = false;
                agent.isStopped = false;
                definirProximoDestino();
            }
        }
    }

    void definirProximoDestino()
    {
        chegou = false;

        GameObject pontosDeRonda = GameObject.FindGameObjectWithTag("PontosDeRonda");
        int numPontosDeRonda = pontosDeRonda.transform.childCount;

        int rand = UnityEngine.Random.Range(0, numPontosDeRonda);
        Transform pontoSelecionado = pontosDeRonda.transform.GetChild(rand);
        agent.SetDestination(pontoSelecionado.position);
        enemySprite.GetComponent<Animator>().SetBool("walking", true);
    }

    void rondar()
    {
        if (chegou)
        {
            if (Time.time > (tempoDeChegada + duracaoDaRonda))
            {
                definirProximoDestino();
            }
        }
        else
        {
            if (agent.remainingDistance <= agent.stoppingDistance + margemDistanciaDestino)
            {
                chegou = true;
                tempoDeChegada = Time.time;
            }
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.up, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
        Vector3 angle02 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if(canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void checarDirecao()
    {
        if (agent.velocity != Vector3.zero)
        {
            // Obter a direção normalizada
            Vector3 direcaoNormalizada = agent.velocity.normalized;

            // Verificar a componente x para determinar a direção
            if (direcaoNormalizada.x < 0)
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            else if (direcaoNormalizada.x > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
}
