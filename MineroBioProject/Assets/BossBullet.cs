using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private GameObject player;
    private BossAI bossAi;
    private Rigidbody2D rb;

    public Collider2D objectCollider;
    public Collider2D otherObjectCollider;

    private int abc = 1;
 
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bossAi = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right*13f;
     
        if (GameObject.ReferenceEquals(this.gameObject,bossAi.sideProjectilePlus))
            {
            //rb.velocity = transform.right*7f; 
            this.rb.angularVelocity = -30f;
            }
       
        if (GameObject.ReferenceEquals(this.gameObject, bossAi.sideProjectileMinus))
        {
            // rb.velocity = transform.right * 7f;
            this.rb.angularVelocity = 30f;

        }
        
        }
    } 

