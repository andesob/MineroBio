using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAi : MonoBehaviour
{
    public enum State
    {
        roaming,
        chase,
        superAttack,
        goingBackToStart

    }
    private int bulletCounter;

    private EnemyMovement enemyMovement;
    public GameObject player;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Vector3 startPos;
    private State state;

    public float detectionDistance;
    public float maxDistance;
    public float shootingDistance;

    private float timeToFire = 0;
    private Quaternion rotation;

    public HealthBar healthBar;

    public int damageFromPistol;
    public float damageTimeout;

    Vector2 shootDirection;
    

    private void Awake()
    {
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        state = State.chase;
        startPos = transform.position;
        shootingDistance = maxDistance + 1f;
        bulletCounter = 0;
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        Rotation();
        shootDirection = -(firePoint.position - player.transform.position).normalized;
        
    }
    private void FixedUpdate()
    {
        
        switch (state)
        {
            case State.roaming:
                print(state);
              
                if (Vector3.Distance(startPos, player.transform.position) < detectionDistance)
                {
                    state = State.chase;
                }

                break;

            case State.chase:
                print(state);
                if(bulletCounter == 4)
                {
                    bulletCounter = 20;
                    SetTimeToFire(2f);
                    state = State.superAttack;
                }
                if (Vector3.Distance(player.transform.position, this.gameObject.transform.position) > maxDistance)
                {
                    enemyMovement.MoveEnemyAfterPlayer();
                    if(Vector3.Distance(player.transform.position, this.gameObject.transform.position) < shootingDistance)
                    {

                        shoot(1f,1);
                        
                    }                
                }             
                else
                {
                    enemyMovement.MoveEnemyAwayPlayer();
                    shoot(1f,1);
                    
                }
                break;
           
            case State.superAttack:
                print(state);
                shoot(100f,-1);
                if(bulletCounter == 0)
                {                   
                    state = State.chase;
                }
                break;
        }
    }

    public void Rotation()
    {
        Vector3 relativePos = player.transform.position - this.gameObject.transform.position;
        rotation = Quaternion.LookRotation(relativePos, Vector3.up);
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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet != null)
        {
            healthBar.healthSystem.Damage(damageFromPistol);
            if (healthBar.healthSystem.getHealth() <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
