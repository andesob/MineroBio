using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    // TODO comment coode

    // The player the enemy follows
    public Transform follewedPlayer;
    public float movementSpeed;

    private Rigidbody2D rb;

    private Vector2 playerDirection;
    private Vector2 spawnDirection;

    public Vector3 startPosition;

    private float towardsPlayerAngle;
    private float towardsSpwanAngle;

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
        RotateAfterPlayer();
        RotateAfterStartingPoint();
    }


    public void MoveEnemyAfterPlayer()
    {
        rotateEnemy(towardsPlayerAngle);

        rb.MovePosition((Vector2)transform.position + (playerDirection * movementSpeed * Time.deltaTime));
    }
    public void MoveEnemyTowardSpawn()
    {
        rotateEnemy(towardsSpwanAngle);
        rb.MovePosition((Vector2)transform.position + (spawnDirection * movementSpeed * Time.deltaTime));
    }

    private void RotateAfterPlayer()
    {
        Vector3 direction = follewedPlayer.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        setAngle(angle, direction);
        direction.Normalize();
        playerDirection = direction;
    }

    public void Roam(Vector2 dir)
    {
        rotateEnemy(towardsSpwanAngle);
        rb.MovePosition((Vector2)transform.position + (dir * movementSpeed * Time.deltaTime));
    }

    private void RotateAfterStartingPoint()
    {
        Vector3 direction = startPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        setAngle(angle, direction);
        direction.Normalize();
        spawnDirection = direction;
    }

    public Vector3 getPlayerPosition()
    {
        return follewedPlayer.position;
    }
    private void setAngle(float angle, Vector3 direction)
    {
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void rotateEnemy(float angle)
    {
        rb.rotation = angle;
    }

    public Vector2 getRandomDir()
    {

        return new Vector2(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f,5f)).normalized;
    }

    private float getRandomAngle()
    {
        return UnityEngine.Random.Range(0, 360);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
