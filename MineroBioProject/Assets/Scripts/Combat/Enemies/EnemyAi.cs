using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private enum State
    {
        Roaming,
        chase,
        GoingBackToStart,
    }

    public HealthBar healthBar;
    public float maximumDistance;

    protected GameObject thisEnemy;
    protected Rigidbody2D thisRigidbody2D;
    protected DamageObject damageObject;
    protected GameObject healthPrefab;
    protected SpawnHealth spawnHealth;
    protected SpawnForceField spawnForceField;
    protected EnemyMovement enemyMovement;
    protected Vector3 startPosition;
    protected float damageTimeout;

    private State blobState;
    private float meleeKnockback = 1f;
    private float rangedKnockback = 1f;
    private bool canTakeDamage = true;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        damageObject = GetComponent<DamageObject>();
        spawnHealth = GetComponent<SpawnHealth>();
        spawnForceField = GetComponent<SpawnForceField>();

        damageTimeout = 0.4f;
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
                if (Vector3.Distance(transform.position, enemyMovement.getStartPosition()) > maximumDistance)
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

    /*
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
    */

    private void CheckIfDead()
    {
        if (healthBar.healthSystem.getHealth() <= 0)
        {
            if (spawnForceField != null)
            {
                spawnForceField.DropItem();
            }
            if (this.gameObject.name != "Boss")
            {

                spawnHealth.DropItem();
            }

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
        if (canTakeDamage)
        {
            healthBar.healthSystem.Damage(dmgAmount);
            StartCoroutine(DamageTimeout(damageTimeout));

            difference = difference.normalized;

            if (isMelee)
            {
                enemyMovement.Knockback(difference, meleeKnockback);
            }
            else
            {
                enemyMovement.Knockback(difference, rangedKnockback);
            }
            CheckIfDead();
        }
    }

}
