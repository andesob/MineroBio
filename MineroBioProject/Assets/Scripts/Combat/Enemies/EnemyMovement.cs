using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    // The player the enemy follows
    public Transform follewedPlayer;
    public float movementSpeed;

    private Rigidbody2D rb;

    private Vector2 playerDirection;
    private Vector2 spawnDirection;

    public Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
    }
    public void MoveEnemyTowardSpawn()
    {
        rb.MovePosition((Vector2)transform.position + (spawnDirection * movementSpeed * Time.deltaTime));
    }

    // Sets the direction for this object to move towards.
    private void SetPlayerDirection()
    {
        Vector3 direction = follewedPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        playerDirection = direction;
    }

    // Sets the direction for this object to move towards the spawn. 
    private void SetSpawnDirection()
    {
        Vector3 direction = startPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        spawnDirection = direction;
    }

    //Returns the main characters position
    public Vector3 GetPlayerPosition()
    {
        return follewedPlayer.position;
    }
  
    // Ignores the collision with the main character. 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}

