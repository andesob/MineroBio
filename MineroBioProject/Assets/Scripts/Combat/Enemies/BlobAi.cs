using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAi : EnemyAi
{
    new public enum State
    {
        roaming,
        chase,
        superAttack,
        goingBackToStart

    }
    private int bulletCounter;

    public GameObject player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float detectionDistance;
    public float shootingDistance;

    private float timeToFire = 0;

    private State blobState;

    Vector2 shootDirection;
    

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        damageObject = GetComponent<DamageObject>();
        thisEnemy = gameObject;
        spawnHealth = GetComponent<SpawnHealth>();
        spawnForceField = GetComponent<SpawnForceField>();
        blobState = State.chase;
        startPosition = transform.position;
        shootingDistance = maximumDistance + 1f;
        bulletCounter = 0;
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        shootDirection = -(firePoint.position - player.transform.position).normalized;
        
    }
    private void FixedUpdate()
    {
        
        switch (blobState)
        {
            case State.roaming:
              
                if (Vector3.Distance(startPosition, player.transform.position) < detectionDistance && enemyMovement.CanMove())
                {
                    blobState = State.chase;
                }

                break;

            case State.chase:
                if (enemyMovement.CanMove())
                {
                    if (bulletCounter == 4)
                    {
                        bulletCounter = 20;
                        SetTimeToFire(2f);
                        blobState = State.superAttack;
                    }
                    if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > maximumDistance)
                    {
                        enemyMovement.MoveEnemyAfterPlayer();
                        if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) < shootingDistance)
                        {

                            shoot(1f, 1);

                        }
                    }
                    else
                    {
                        enemyMovement.MoveEnemyAwayPlayer();
                        shoot(1f, 1);

                    }
                }
                break;
           
            case State.superAttack:
                if (enemyMovement.CanMove())
                {
                    shoot(100f, -1);
                    if (bulletCounter == 0)
                    {
                        blobState = State.chase;
                    }
                }
                break;
        }
    }

    public void SetTimeToFire(float fireRate)
    {
        timeToFire = Time.time + 1 / fireRate;
    }
    public void shoot(float fireRate,int bulletUpDown)
    {
        if (Time.time >= timeToFire)
        {
            SetTimeToFire(fireRate);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shootDirection * 5f, ForceMode2D.Impulse);
            bulletCounter += bulletUpDown;
        }
    }
}
