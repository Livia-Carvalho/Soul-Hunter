using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterScript : MonoBehaviour
{
    private PlayerAttackScript playerAttackScript;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttackScript = player.GetComponent<PlayerAttackScript>();

        Vector3 direcaoDoArremesso = new Vector3(playerAttackScript.playerLasHorizontalMovement, playerAttackScript.playerLastVerticalMovement, 0);

        Debug.Log("vertical = " + playerAttackScript.playerLastVerticalMovement.ToString());
        Debug.Log("direcao = " + direcaoDoArremesso.ToString());
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
            Destroy(gameObject);
        }
    }
}
