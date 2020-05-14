using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for the boss enemy that extends the enemyAI script. 
 * This script decides when the boss will attack and which attack to use.
 */
public class BossAI : EnemyAi
{
    private enum State
    {
        idle,
        teleportAttack,
        secondTeleportAttack,
        superAttack
    }

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject teleporter;
    public GameObject gooProjectile;
    public Animator anim;
    public float maxDistance;

    protected internal GameObject sideProjectilePlus;
    protected internal GameObject sideProjectileMinus;
    protected internal GameObject mainProjectile;
    protected internal GameObject teleportIn;
    protected internal GameObject teleportOut;
    protected internal bool hasHit = false;
    protected internal int teleportCounter = 4;
    protected internal List<Vector3> gooProjectiles = new List<Vector3>();

    private State state;
    private GameObject player;
    private Vector3 originalPos;
    private Vector3 spawnPoint;
    private Quaternion rotation;
    private float timeToFire;
    private float timeOut;
    private float teleportChance;
    private float superAttackChance;
    private int gooProjectileCounter = 3;
    private int shootCount = 2;
    private int superAttackCount = 3;
    private int timeToNextTeleport;
    private bool hasTeleported;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        state = State.idle;
        hasTeleported = false;
        maxDistance = 7f;
        timeToNextTeleport = 3;
        superAttackChance = 1f;
        player = GameObject.FindGameObjectWithTag("Player");
        thisEnemy = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.x > player.transform.position.x + 0.1f)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    private void FixedUpdate()
    {
        switch (state)
        {
            case State.idle:
                if (Time.time >= timeOut)
                {

                    if (hasTeleported)
                    {
                        transform.position = originalPos;
                        hasTeleported = false;
                    }

                    if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > maxDistance)
                    {
                        shoot(4f);
                        enemyMovement.MoveEnemyAfterPlayer();
                        anim.Play("BossRun");
                    }
                    else
                    {
                        shoot(4f);
                        enemyMovement.MoveEnemyAwayPlayer();
                        anim.Play("BossRun");
                    }

                    if (superAttackChance < 0.1f)
                    {
                        timeOut = Time.time + 0.7f;
                        superAttackCount = 3;
                        state = State.superAttack;
                    }

                    if (timeToNextTeleport <= 0 && teleportChance < 0.3f)
                    {
                        originalPos = transform.position;
                        timeOut = Time.time + 1.3f;
                        timeToNextTeleport = 3;
                        gooProjectileCounter = 4;
                        hasHit = false;
                        state = State.teleportAttack;
                    }
                }
                break;

                //Teleports the boss to another location and then teleports back again
            case State.teleportAttack:
                if (gooProjectileCounter > 0)
                {
                    spawnGooProjectile();
                }
                if (hasHit)
                {
                    if (gooProjectileCounter == 0 && Time.time >= timeOut)
                    {
                        int randIndex = Random.Range(0, gooProjectiles.Count);
                        spawnPoint = gooProjectiles[randIndex];
                        transform.position = spawnPoint;
                        hasTeleported = true;
                        TripleAttack();
                        float teleportAgain = Random.Range(0f, 1f);

                        if (teleportAgain < 1f)
                        {
                            gooProjectiles.RemoveAt(randIndex);
                            randIndex = Random.Range(0, gooProjectiles.Count);
                            spawnPoint = gooProjectiles[randIndex];
                            gooProjectiles.RemoveRange(0, gooProjectiles.Count);
                            timeOut = Time.time + 0.7f;
                            state = State.secondTeleportAttack;
                        }
                        else
                        {
                            timeOut = Time.time + 0.7f;
                            state = State.idle;
                        }
                    }
                }
                break;

            case State.secondTeleportAttack:
                if (Time.time >= timeOut)
                {
                    transform.position = spawnPoint;
                    TripleAttack();
                    timeOut = Time.time + 0.7f;
                    state = State.idle;
                }
                break;

            case State.superAttack:
                if (Time.time > timeOut && superAttackCount > 0)
                {
                    ShootSuperAttack();
                    timeOut = Time.time + 0.5f;
                    superAttackCount -= 1;
                }

                if (superAttackCount == 0)
                {
                    superAttackChance = 1f;
                    state = State.idle;
                }
                break;
        }
    }


    public void TripleAttack()
    {
        setFirePoint();
        if (Time.time >= timeOut)
        {
            mainProjectile = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
            sideProjectilePlus = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 25f));
            sideProjectileMinus = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(0f, 0f, -25f));
        }
    }

    public void shoot(float fireRate)
    {
        setFirePoint();
        if (Time.time >= timeToFire && shootCount > 0)
        {
            SetTimeToFire(fireRate);
            mainProjectile = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
            anim.StopPlayback();
            anim.Play("BossAttack");
            shootCount -= 1;
            timeToNextTeleport -= 1;
        }
        if (shootCount == 0 && Time.time >= timeToFire)
        {
            SetTimeToFire(0.8f);
            mainProjectile = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
            mainProjectile = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 15f));
            mainProjectile = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(0f, 0f, -15f));
            shootCount = 2;
            teleportChance = Random.Range(0f, 1f);
            superAttackChance = Random.Range(0f, 1f);
            timeToNextTeleport -= 1;
        }
    }

    public void setFirePoint()
    {
        Vector3 direction = player.transform.position - firePoint.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        firePoint.rotation = rotation;
    }

    public void spawnGooProjectile()
    {
        if (gooProjectileCounter > 3)
        {
            teleportIn = Instantiate(gooProjectile, transform.position, transform.rotation);
            teleportIn.gameObject.tag = "BossTeleportIn";
            gooProjectileCounter -= 1;
        }
        else
        {
            GameObject teleportOut = Instantiate(gooProjectile, transform.position, transform.rotation);
            gooProjectileCounter -= 1;
        }

    }

    public float GetRandomRotation()
    {
        float randomRotation = Random.Range(0, 360);
        float numSteps = Mathf.Floor(randomRotation / 30f);
        float adjustedRotation = numSteps * 30f;

        return adjustedRotation;
    }

    public void SetTimeToFire(float fireRate)
    {
        timeToFire = Time.time + 1 / fireRate;
    }

    public void ShootSuperAttack()
    {
        for (int i = 0; i < 20; i++)
        {
            SetTimeToFire(0.3f);
            float angle = i * Mathf.PI * 2 / 20 + superAttackCount;
            Vector3 pos = new Vector3(Mathf.Cos(angle) + transform.position.x, Mathf.Sin(angle) + transform.position.y * 1, 0);
            Vector3 dir = pos - transform.position;
            float rotAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.AngleAxis(rotAngle, Vector3.forward);
            mainProjectile = Instantiate(bulletPrefab, pos, rot);
        }
    }
}