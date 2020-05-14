using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for teleporting the boss
 */
public class BossTeleportProjectileScript : MonoBehaviour
{
    public GameObject hitPrefab;

    protected internal bool hasHit = false;

    private BossAI bossAi;
    private float randX;
    private float randY;
    private float timeOut;

    // Start is called before the first frame update
    void Start()
    {
        randX = Random.Range(-1f, 1f);
        randY = Random.Range(-1f, 1f);
        timeOut = Time.time + 0.7f;

        bossAi = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time <= timeOut && !this.gameObject.CompareTag("BossTeleportIn"))
        {
            transform.position += new Vector3(randX, randY).normalized * Time.deltaTime * 6f;
        }
        else
        {
            DestroyAfterTime();
        }
    }

    private void DestroyAfterTime()
    {
        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            hitVFX.transform.GetChild(4).GetComponent<Transform>().transform.rotation = Quaternion.Euler(0, 0, bossAi.GetRandomRotation());
            bossAi.hasHit = true;
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

        Vector3 lastKnownPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        bossAi.gooProjectiles.Add(lastKnownPosition);
        Destroy(gameObject);
    }
}
