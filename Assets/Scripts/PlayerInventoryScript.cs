using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventoryScript : MonoBehaviour
{

    public int playerHP = 5;
    public int playerHolyWaterAmmo = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerHP();
    }

    private void checkPlayerHP()
    {
        if(playerHP <= 0) 
        {
            SceneManager.LoadScene("Derrota");
        }
    }
}
