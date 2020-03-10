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
    private Rigidbody2D thisRigidbody2D;
    private EnemyMovement enemyMovement;
    private DamageObject damageObject;
    private Vector3 startPosition;
    private State state;
    private bool canTakeDamage = true;

    public float maximumDistance;
    public int damageFromPistol;
    public float damageTimeout;
    public HealthBar healthBar;
    public float knockbackDistance;
    public float knockbackTime;

    private int MELEE_DAMAGE = 25;


    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        damageObject = GetComponent<DamageObject>();
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
                if (Vector3.Distance(startPosition, enemyMovement.GetPlayerPosition()) < maximumDistance && enemyMovement.CanMove())
                {
                    state = State.chase;
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
                    state = State.GoingBackToStart;
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
                    state = State.chase;
                }
                if (Vector3.Distance(transform.position, startPosition) < 1f)
                {
                    state = State.Roaming;
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
            difference = difference.normalized * knockbackDistance;

            enemyMovement.Knockback(difference);

            TakeDamage(MELEE_DAMAGE);
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
        state = State.chase;
    }

    public void TakeDamage(int dmgAmount)
    {
        healthBar.healthSystem.Damage(dmgAmount);
    }

}
