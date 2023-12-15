using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DemonScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 8f;
    private Transform targetFace;
    public bool movingToFace = false;
    public bool chegou = false;
    [SerializeField] private float margemDistancia = 0.5f;
    public int demonHP = 3;
    public Transform inimigoPossuido;
    private bool jaPossuiuAlguem = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingToFace && !chegou)
        {
            moverAteRosto(inimigoPossuido);
        }else if(chegou)
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

    public void makeMaskInvisible()
    { 
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
        
    public void chooseRandomNPC()
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

        inimigoPossuido = inimigoSelecionado;
        moverAteRosto(inimigoPossuido);
        possuir(inimigoSelecionado);
    }

    void moverAteRosto(Transform rosto)
    {

        Vector2 novaPosicao = Vector2.MoveTowards(transform.position, rosto.position, moveSpeed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(novaPosicao);
        movingToFace = true;

        Debug.Log(rosto);

        if (Vector2.Distance(rosto.position, gameObject.transform.position) <= margemDistancia)
        {
            chegou = true;
            movingToFace = false;
        }
    }

    public void moverAoTopo(Transform inimigoPossuido)
    {
        transform.position = Vector2.MoveTowards(inimigoPossuido.position, new Vector3(-41.4000015f, 22.75f, -0.0218398441f), moveSpeed * Time.deltaTime);
    }
}
