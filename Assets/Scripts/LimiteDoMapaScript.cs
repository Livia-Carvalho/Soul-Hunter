using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteDoMapaScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Demon") { }
        {
            collision.transform.position = new Vector3(0, 0, 0);
            collision.transform.GetComponent<DemonScript>().makeMaskInvisible();
            collision.transform.GetComponent<DemonScript>().chooseRandomNPC();
            collision.transform.GetComponent<DemonScript>().chegou = false;
            collision.transform.GetComponent<DemonScript>().movingToFace = false;
        }
    }
}
