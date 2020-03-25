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

    protected GameObject thisEnemy;
    protected Rigidbody2D thisRigidbody2D;
    protected DamageObject damageObject;
    private State blobState;
    private bool canTakeDamage = true;

    protected EnemyMovement enemyMovement;
    protected Vector3 startPosition;

    private float meleeKnockback = 3f;
    private float rangedKnockback = 50f;

    public float maximumDistance;
    private float damageTimeout = 1f;
    public HealthBar healthBar;


    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        damageObject = GetComponent<DamageObject>();
        blobState = State.Roaming;
        startPosition = transform.position;
        thisEnemy = gameObject;
    }
    private void FixedUpdate()
    {
        switch (blobState)
        {
            // The player stays in its starting area
            // TODO get a movement when it stays static.
            case State.Roaming:
                if (Vector3.Distance(startPosition, enemyMovement.GetPlayerPosition()) < maximumDistance && enemyMovement.CanMove())
                {
                    blobState = State.chase;
                }
                break;
            // This state is made to chase after the main character. Will change when this object reaches the maximum distance. 
            case State.chase:
                if (enemyMovement.CanMove() && damageObject.CanDamage())
                {
                    enemyMovement.MoveEnemyAfterPlayer();
                }
                if (Vector3.Distance(transform.position, enemyMovement.startPosition) > maximumDistance)
                {
                    blobState = State.GoingBackToStart;
                }
                break;

            // This state moves the enemy back towards the spawning point of the object. 
            case State.GoingBackToStart:
                if (enemyMovement.CanMove())
                {
                    enemyMovement.MoveEnemyTowardSpawn();
                }
                if (Vector3.Distance(startPosition, enemyMovement.GetPlayerPosition()) < maximumDistance)
                {
                    blobState = State.chase;
                }
                if (Vector3.Distance(transform.position, startPosition) < 1f)
                {
                    blobState = State.Roaming;
                }
                break;
        }
    }

    //TODO add the tags for the other weapons, and implement different knockback and damageTimout for each weapon. 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        string colliderTag = collider.gameObject.tag;
        if (colliderTag == "Melee" && canTakeDamage)
        {
            Vector2 difference = thisRigidbody2D.transform.position - collider.transform.position;
            difference = difference.normalized;

            StartCoroutine(DamageTimeout(damageTimeout));
        }
        CheckIfDead();
    }


    private void CheckIfDead()
    {
        if (healthBar.healthSystem.getHealth() <= 0)
        {
            Destroy(thisEnemy);
        }
    }

    private IEnumerator DamageTimeout(float timeout)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(timeout);
        canTakeDamage = true;
        blobState = State.chase;
    }

    public void TakeDamage(int dmgAmount, Vector2 difference, bool isMelee)
    {
        healthBar.healthSystem.Damage(dmgAmount);
        difference = difference.normalized;

        if (isMelee)
        {
        Debug.Log(difference);
            enemyMovement.Knockback(difference, meleeKnockback);
        }
        else
        {
            enemyMovement.Knockback(difference, rangedKnockback);
        }
        CheckIfDead();
    }

}
