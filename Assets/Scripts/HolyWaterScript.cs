using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterScript : MonoBehaviour
{
    private PlayerAttackScript playerAttackScript;
    private PlayerMovementScript playerMovementScript;
    private GameObject player;
    [SerializeField] private EnemyScript enemyScript;
    DemonScript demonScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<PlayerAttackScript>();
        playerMovementScript = player.GetComponent<PlayerMovementScript>();
        demonScript = GameObject.FindGameObjectsWithTag("Demon")[0].GetComponent<DemonScript>();

        Vector3 direcaoDoArremesso = Vector3.zero;

        if (playerMovementScript.verticalInput != 0 || playerMovementScript.horizontalInput != 0)
        {
            direcaoDoArremesso = new Vector3(playerMovementScript.verticalInput, playerMovementScript.horizontalInput, 0);
        }
        else if (playerMovementScript.verticalInput == 0 && playerMovementScript.horizontalInput == 0)
        {
            direcaoDoArremesso = new Vector3(playerAttackScript.playerLasHorizontalMovement, playerAttackScript.playerLastVerticalMovement, 0);
        }

        gameObject.GetComponent<Rigidbody2D>().AddForce(direcaoDoArremesso * playerAttackScript.forcaDoArremesso);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Holy Water")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, 10f);
        }

        if(collision.tag == "Inimigo")
        {
            
            if (enemyScript.checarPossuido(collision.gameObject))
            {
                Debug.Log("funcao retornou verdadeiro");
                demonScript.moverAoTopo(demonScript.inimigoPossuido);
            }
        }
    }
}
