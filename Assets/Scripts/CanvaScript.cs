using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvaScript : MonoBehaviour
{

    private PlayerInventoryScript playerInventoryScript;

    // Start is called before the first frame update
    void Start()
    {
        playerInventoryScript = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerInventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "HP: " + playerInventoryScript.playerHP + "\nHoly Water: " + playerInventoryScript.playerHolyWaterAmmo;
    }
}
