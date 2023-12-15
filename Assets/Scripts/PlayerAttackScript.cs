using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{

    public float playerLastVerticalMovement = 0;
    public float playerLasHorizontalMovement = 1;
    private GameObject playerSprite;

    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackCooldown = 1f;
    private float lastAttackTime = 0;
    [SerializeField] private LayerMask enemiesLayerMask;

    [SerializeField] private GameObject holyWaterPrefab;
    public float forcaDoArremesso = 500;

    bool attacking = false;


    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GameObject.FindGameObjectsWithTag("PlayerSprite")[0];
        attackPos.position = gameObject.transform.position + new Vector3(0.5f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            attacking = true;
            attack();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            attacking = false;
            attackAnimation();
        } else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Instantiate(holyWaterPrefab, attackPos.position, transform.rotation);
        }
    }

    void attackAnimation()
    {

        if (attacking == true)
        {
            playerSprite.GetComponent<Animator>().SetBool("attacking", true);
            
        } else
        {
            playerSprite.GetComponent<Animator>().SetBool("attacking", false);
        }
        
    }

    void attack()
    {
        
        if(Time.time > (lastAttackTime + attackCooldown))
        {
            attackAnimation();

            Collider2D[] attackedEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemiesLayerMask);
            for (int i = 0; i < attackedEnemies.Length; i++)
            {
                attackedEnemies[i].GetComponent<EnemyScript>().serAtacado();
            }

            attacking = false;
            lastAttackTime = Time.time;
        }
        
    }

    public void moveAttackPosition(float verticalDirection, float horizontalDirection)
    {
        if(verticalDirection != 0 || horizontalDirection != 0)
        {
            attackPos.position = gameObject.transform.position + new Vector3(horizontalDirection * 0.5f, verticalDirection * 1f, 0);
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
