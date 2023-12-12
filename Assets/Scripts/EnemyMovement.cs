using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent agent;

    public bool rondando = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(rondando)
        {
            rondar();
        }

        agent.SetDestination(target.position);
    }

    void rondar()
    {
        GameObject pontosDeRonda = GameObject.FindGameObjectWithTag("PontosDeRonda");
        int numPontosDeRonda = pontosDeRonda.transform.childCount;

        int rand = Random.Range(0, numPontosDeRonda);
        Transform pontoSelecionado = pontosDeRonda.transform.GetChild(rand);
        agent.SetDestination(pontoSelecionado.position);

    }
}
