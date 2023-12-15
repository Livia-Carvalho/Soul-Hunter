using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float runningMultiplier = 1.5f;
    private GameObject playerSprite;
    public float horizontalInput = 0;
    public float verticalInput = 0;

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
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            finalMovementSpeed = movementSpeed * runningMultiplier;
            running = true;
        }

        PlayerAttackScript playerAttackScript = gameObject.GetComponent<PlayerAttackScript>();
        playerAttackScript.moveAttackPosition(verticalInput, horizontalInput);

        playerAttackScript.playerLastVerticalMovement = verticalInput;
        if ((horizontalInput != 0 && verticalInput == 0) || (horizontalInput == 0 && verticalInput != 0))
        {
            playerAttackScript.playerLasHorizontalMovement = horizontalInput;
            playerAttackScript.playerLastVerticalMovement = verticalInput;
        } else if(horizontalInput != 0 && verticalInput != 0)
        {
            playerAttackScript.playerLasHorizontalMovement = horizontalInput;
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
