using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    [SerializeField] private int numInimigos = 2;
    [SerializeField] private GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numInimigos; i++)
        {
            GameObject inimigosContainer = GameObject.FindGameObjectWithTag("Inimigos");

            GameObject pontosDeRonda = GameObject.FindGameObjectWithTag("PontosDeRonda");
            int numPontosDeRonda = pontosDeRonda.transform.childCount;

            int rand = UnityEngine.Random.Range(0, numPontosDeRonda);
            Transform pontoSelecionado = pontosDeRonda.transform.GetChild(rand);

            Instantiate(enemyPrefab, pontoSelecionado.position, pontoSelecionado.rotation, inimigosContainer.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
