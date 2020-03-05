using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public enum State
    {
        Roaming,
        chase,
        GoingBackToStart,
    }

    private GameObject thisEnemy;
    private EnemyMovement enemyMovement;
    private Vector3 startPosition;
    private State state;

    public float maximumDistance;
    public int damageFromPistol;
    public float damageTimeout;
    public HealthBar healthBar;
    public float knockbackDistance;
  

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        state = State.Roaming;
        startPosition = transform.position;
        thisEnemy = gameObject;
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            // The player stays in its starting area
            // TODO get a movement when it stays static.
            case State.Roaming:
                if (Vector3.Distance(startPosition, enemyMovement.GetPlayerPosition()) < maximumDistance)
                {
                    state = State.chase;
                }
                break;
            // This state is made to chase after the main character. Will change when this object reaches the maximum distance. 
            case State.chase:
                enemyMovement.MoveEnemyAfterPlayer();
                if (Vector3.Distance(transform.position, enemyMovement.startPosition) > maximumDistance)
                {
                    state = State.GoingBackToStart;
                }
                break;

            // This state moves this object back towards the spawning point of the object. 
            case State.GoingBackToStart:
                enemyMovement.MoveEnemyTowardSpawn();
                if (Vector3.Distance(startPosition, enemyMovement.GetPlayerPosition()) < maximumDistance)
                {
                    state = State.chase;
                }   
                if (Vector3.Distance(transform.position, startPosition) < 1f)
                {
                    state = State.Roaming;
                }
                break;
        }

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        string colliderTag = collider.gameObject.tag;
            if (colliderTag == "Melee")
            {

            //Other.rigidbody.AddForce(bulletDir.normalized * force);
            // enemyMovement.Knockback(bulletDir, knockbackDistance);

            healthBar.healthSystem.Damage(25);
          
            }
            else if(colliderTag == "Bullet")
            {
                healthBar.healthSystem.Damage(damageFromPistol);
            }
        checkIfDead();

           
    }
    private void checkIfDead()
    {
        if (healthBar.healthSystem.getHealth() <= 0)
        {
            Destroy(thisEnemy);
        }
    }

}
