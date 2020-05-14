using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used for controlling the bullets that the boss shoot
 */
public class BossBullet : MonoBehaviour
{
    public GameObject hitPrefab;
    public GameObject tip;

    private BossAI bossAi;
    private Rigidbody2D rb;
    private Vector3 originalPos;
    private float time;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bossAi = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
        time = Time.time;
        originalPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * 13f;

        if (GameObject.ReferenceEquals(this.gameObject, bossAi.sideProjectilePlus))
        {
            this.rb.angularVelocity = -30f;
        }

        if (GameObject.ReferenceEquals(this.gameObject, bossAi.sideProjectileMinus))
        {
            this.rb.angularVelocity = 30f;
        }
    }

    //If the bullet hits something destroy the gameobject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Bullet") && Time.time > time)
        {
            time = Time.time + 1;
            Vector2 pos = tip.transform.position;
            Vector3 dir = -transform.position - originalPos;
            float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(ang, Vector3.forward);

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rotation);
                var psHit = hitVFX.GetComponent<ParticleSystem>();

                if (psHit != null)
                {
                    Destroy(hitVFX, psHit.main.duration);
                }
                else
                {
                    var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitVFX, psChild.main.duration);
                }
            }
            Destroy(gameObject);
        }
    }
}

