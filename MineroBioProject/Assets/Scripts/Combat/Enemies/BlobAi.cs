﻿using System.Collections;
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

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float detectionDistance;

    private State blobState;
    private GameObject player;
    private Vector2 shootDirection;
    private float timeToFire = 0;
    private float shootingDistance;
    private int bulletCounter;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyMovement = GetComponent<EnemyMovement>();
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        damageObject = GetComponent<DamageObject>();
        thisEnemy = gameObject;
        blobState = State.roaming;
        spawnHealth = GetComponent<SpawnHealth>();
        spawnForceField = GetComponent<SpawnForceField>();
        damageTimeout = 0.4f;
        startPosition = transform.position;
        shootingDistance = maximumDistance + 1f;
        bulletCounter = 0;
    }
  

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

                /*
                 * Chase after the player position and shoot when in range
                 * If the blob has shot 4 bullets then use super attack
                 */
            case State.chase:
                if (enemyMovement.CanMove())
                {
                    if (bulletCounter == 4)
                    {
                        bulletCounter = 5;
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
