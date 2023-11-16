using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{

    private GameObject playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GameObject.FindGameObjectsWithTag("PlayerSprite")[0];
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }

    void attack()
    {
        bool attacking = false;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            attacking = true;
        }

        if (attacking == true)
        {
            playerSprite.GetComponent<Animator>().SetBool("attacking", true);
        } else
        {
            playerSprite.GetComponent<Animator>().SetBool("attacking", false);
        }
        
    }
}
