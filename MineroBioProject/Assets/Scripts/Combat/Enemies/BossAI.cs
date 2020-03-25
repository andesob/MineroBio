using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public enum State
    {
        idle,
        beforeTeleport,
        teleportAttack,
        secondTeleportAttack,
        shoot
    }

    private State state;
    private Vector3 startPos;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject player;

    protected internal bool hasHit = false;

    private int gooProjectileCounter = 3;

    private Vector3 originalPos;

    protected internal int teleportCounter = 4;
    List<GameObject> teleporters = new List<GameObject>();
    protected internal List<Vector3> gooProjectiles = new List<Vector3>();
    public GameObject teleporter;
    public GameObject gooProjectile;

    protected internal GameObject sideProjectilePlus;
    protected internal GameObject sideProjectileMinus;
    protected internal GameObject mainProjectile;

    protected internal GameObject teleportIn;
    protected internal GameObject teleportOut;

    private bool hasTeleported;

    private Vector3 spawnPoint;
    private int randIndex;

    private float timeOut;

    public float maxDistance;

    public Vector2 shootDirection;

    Quaternion rotation;

    private BossTeleportProjectileScript bossTeleportProjectileScript = new BossTeleportProjectileScript();
    private EnemyMovement enemyMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        state = State.beforeTeleport;
        startPos = transform.position;
        hasTeleported = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(state);
    }

    private void FixedUpdate()
    {

        switch (state)
        {
            case State.idle:
                if (Time.time >= timeOut) {
                    if (hasTeleported)
                    {
                        transform.position = originalPos;
                        hasTeleported = false;
                    }
                    if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > maxDistance)
                    {
                        enemyMovement.MoveEnemyAfterPlayer();
                    }
                    else
                    {
                        enemyMovement.MoveEnemyAwayPlayer();
                    }
                }
                break;

            case State.beforeTeleport:
                originalPos = transform.position;
                timeOut = Time.time + 1.3f;
                state = State.teleportAttack;
                break;

            case State.teleportAttack:

                if (gooProjectileCounter > 0)
                {
                    spawnGooProjectile();
                }
                print(hasHit);
                if (hasHit)
                {
                    while (teleportCounter > 0)
                    {
                        spawnTeleporter();

                    }

                    if (gooProjectileCounter == 0 && Time.time >= timeOut)
                    {

                        //  teleporters.AddRange(GameObject.FindGameObjectsWithTag("BossTeleportOut"));
                        randIndex = Random.Range(0, gooProjectiles.Count);
                        spawnPoint = gooProjectiles[randIndex];
                        transform.position = spawnPoint;
                        hasTeleported = true;
                        setFirePoint();
                        shoot();
                        float teleportAgain = Random.Range(0f, 1f);
                        if (teleportAgain < 1f)
                        {
                            gooProjectiles.RemoveAt(randIndex);
                            randIndex = Random.Range(0, gooProjectiles.Count);
                            spawnPoint = gooProjectiles[randIndex];

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
                    setFirePoint();
                    shoot();
                    timeOut = Time.time + 0.7f;
                    state = State.idle;
                }
                break;
        }
    }
    private void spawnTeleporter()
    {
        if (teleportCounter > 3)
        {
            teleportIn = Instantiate(teleporter, transform.position, transform.rotation);
            teleportIn.gameObject.tag = "BossTeleportIn";
            teleportCounter -= 1;
        }
        foreach(Vector3 i in gooProjectiles)
        {

            teleportOut = Instantiate(teleporter, i, Quaternion.Euler(0,0,GetRandomRotation()));
            teleportCounter -= 1;
        }
      

        teleportCounter -= 1;
    }

    public void shoot()
    {

        if (Time.time >= timeOut)
        {
            mainProjectile = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
            if (state == State.teleportAttack || state == State.secondTeleportAttack)
            {


                sideProjectilePlus = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(0f, 0f, 25f));
                sideProjectileMinus = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation * Quaternion.Euler(0f, 0f, -25f));
            }
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
        gooProjectile = Instantiate(gooProjectile, transform.position, transform.rotation);
        gooProjectileCounter -= 1;
    }
    private float GetRandomRotation()
    {
        float randomRotation = Random.Range(0, 360);
        float numSteps = Mathf.Floor(randomRotation / 30f);
        float adjustedRotation = numSteps * 30f;

        return adjustedRotation;
    }
  
    
}
