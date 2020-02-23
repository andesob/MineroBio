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
    

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        state = State.chase;
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
            Bullet bullet = collider.GetComponent<Bullet>();
            if (bullet != null)
            {
                healthBar.healthSystem.Damage(damageFromPistol);
            if (healthBar.healthSystem.getHealth() <= 0)
            {
                Destroy(thisEnemy);
            }
        }
        }




}
