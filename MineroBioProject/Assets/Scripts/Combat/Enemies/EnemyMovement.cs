﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    // The player the enemy follows
    private Transform mainCharacter;
    public float movementSpeed;

    private Rigidbody2D rb;

    private Vector2 playerDirection;
    private Vector2 spawnDirection;
    private Rigidbody2D thisRigidbody2D;
    private bool canMove = true;
    private float knockbackDistance = 0.3f;

    public Vector3 startPosition;
    private float knockbacktime = 0.3f;

    private void Awake()
    {
        startPosition = transform.position;
        thisRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        if (this.gameObject.transform.position.x > mainCharacter.transform.position.x + 0.1f)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerDirection();
        SetSpawnDirection();
    }

    public void MoveEnemyAfterPlayer()
    {
        rb.MovePosition((Vector2)transform.position + (playerDirection * movementSpeed * Time.deltaTime));
        if (this.gameObject.transform.position.x > mainCharacter.transform.position.x + 0.1f)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
    public void MoveEnemyAwayPlayer()
    {
        rb.MovePosition((Vector2)transform.position - (playerDirection * movementSpeed * Time.deltaTime));
        if (this.gameObject.transform.position.x > follewedPlayer.transform.position.x + 0.1f)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
    public void MoveEnemyTowardSpawn()
    {
        rb.MovePosition((Vector2)transform.position + (spawnDirection * movementSpeed * Time.deltaTime));
    }

    // Sets the direction for this object to move towards.
    private void SetPlayerDirection()
    {
        Vector3 direction = mainCharacter.position - transform.position;
        direction.Normalize();
        playerDirection = direction;
    }

    // Sets the direction for this object to move towards the spawn. 
    private void SetSpawnDirection()
    {
        Vector3 direction = startPosition - transform.position;
        direction.Normalize();
        spawnDirection = direction;
    }

    //Returns the main characters position
    public Vector3 GetPlayerPosition()
    {
        return mainCharacter.position;
    }
  
    // Ignores the collision with the main character. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    //Knoks the enemy back. 
    public void Knockback(Vector2 difference)
    {
         thisRigidbody2D.AddForce(difference*knockbackDistance, ForceMode2D.Impulse);
         StartCoroutine(KnockbackTimer(thisRigidbody2D));
     } 

    //Sets the time for how long the enemy rigidbody should move
    private IEnumerator KnockbackTimer(Rigidbody2D thisRigidbody2D)
    {
        canMove = false;
        yield return new WaitForSeconds(knockbacktime);
        thisRigidbody2D.velocity = Vector2.zero;
        canMove = true;
        
    }
    // Returns canMove
    public bool CanMove()
    {
        return canMove;
    }
}

