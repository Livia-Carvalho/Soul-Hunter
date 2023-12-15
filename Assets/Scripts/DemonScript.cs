using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DemonScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 8f;
    private Transform targetFace;
    private bool movingToFace = false;
    private bool chegou = false;
    [SerializeField] private float margemDistancia = 0.5f;
    public int demonHP = 3;
    private Transform inimigoPossuido;
    private bool jaPossuiuAlguem = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (!movingToFace)
        {
            chooseRandomNPC();
        }
        else
        {
            checarChegada();
        }

        if (chegou)
        {
            makeMaskInvisible();
        }
    }

    private void possuir(Transform inimigo)
    {
        EnemyScript script = inimigo.GetComponent<EnemyScript>();
        inimigoPossuido = inimigo;

        script.possuido = true;
        script.enemyHP = 99;

        if(!jaPossuiuAlguem)
        {
            jaPossuiuAlguem = true;
        }
    }

    private void makeMaskInvisible()
    { 
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    private void checarChegada()
    {
        if (!chegou)
        {
            moverAteRosto(targetFace);
        }
    }

    void chooseRandomNPC()
    {
        GameObject inimigos = GameObject.FindGameObjectWithTag("Inimigos");
        int numDeInimigos = inimigos.transform.childCount;

        Transform inimigoSelecionado = null;
        if (!jaPossuiuAlguem){

            int rand = UnityEngine.Random.Range(0, numDeInimigos);
            inimigoSelecionado = inimigos.transform.GetChild(rand);
        } else
        {
            bool temp = false;

            do
            {
                int rand = UnityEngine.Random.Range(0, numDeInimigos);
                inimigoSelecionado = inimigos.transform.GetChild(rand);

                EnemyScript scriptInimigoSelecionado = inimigoSelecionado.GetComponent<EnemyScript>();
                EnemyScript scriptInimigoPossuido = inimigoPossuido.GetComponent<EnemyScript>();

                if (scriptInimigoSelecionado.possuido == false)
                {
                    scriptInimigoPossuido.possuido = false;
                    temp = true;
                }
            } while(!temp);
        }

        foreach (Transform t in inimigoSelecionado.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("RostoInimigo"))
            {
                targetFace = t;
            }
        }

        moverAteRosto(targetFace);
        possuir(inimigoSelecionado);
    }

    void moverAteRosto(Transform rosto)
    {
        float distance = Vector2.Distance(transform.position, rosto.position);
        Vector2 direcao = (rosto.position - gameObject.transform.position).normalized;

        transform.position = Vector2.MoveTowards(this.transform.position, rosto.position, moveSpeed * Time.deltaTime);
        movingToFace = true;

        if (Vector2.Distance(targetFace.position, gameObject.transform.position) <= margemDistancia)
        {
            chegou = true;
            movingToFace = false;
        }
    }
}
