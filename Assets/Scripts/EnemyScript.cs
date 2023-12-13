using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{

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

    void nocaute()
    {
        agent.isStopped = true;

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

}
