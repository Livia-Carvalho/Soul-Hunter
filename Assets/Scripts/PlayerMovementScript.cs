using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float runningMultiplier = 1.5f;
    private GameObject playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GameObject.FindGameObjectsWithTag("PlayerSprite")[0];
    }

    // Update is called once per frame
    void Update()
    {

        playerMovement();

    }

    void playerMovement()
    {
        float finalMovementSpeed = movementSpeed;
        bool running = false;

        //Check keys
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            finalMovementSpeed = movementSpeed * runningMultiplier;
            running = true;
        }

        //Set animations
        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerSprite.GetComponent<Animator>().SetBool("walking", false);
        } 
        else{
            playerSprite.GetComponent<Animator>().SetBool("walking", true);
        }

        if(running != true)
        {
            playerSprite.GetComponent<Animator>().SetBool("running", false);
        }
        else
        {
            playerSprite.GetComponent<Animator>().SetBool("running", true);
        }

        //Get current location
        float currentHorizontalPosition = transform.position.x;
        float currentVerticalPosition = transform.position.y;

        //Make player look left or right based on movement
        if(horizontalInput < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        } 
        else if(horizontalInput > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        //Move player
        Vector3 targetPosition = new Vector3((currentHorizontalPosition + horizontalInput), (currentVerticalPosition + verticalInput), 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, finalMovementSpeed * Time.deltaTime);
    }

}
