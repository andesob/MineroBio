using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleportProjectileScript : MonoBehaviour
{
    private float randX;
    private float randY;
    public GameObject hitPrefab;

    protected internal bool hasHit = false;

    private BossAI bossAi;
    // Start is called before the first frame update
    void Start()
    {
         randX = Random.Range(-1f, 1f);
         randY = Random.Range(-1f, 1f);
 
  
        bossAi = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossAI>();
     
    }

    // Update is called once per frame
     void Update()
     {
        /* if(GameObject.ReferenceEquals(this.gameObject,bossAi.teleportIn))
         {

         }
         else*/ if(Time.time <= 0.7f)
         {
             transform.position += new Vector3(randX,randY).normalized * Time.deltaTime * 6f;
         }
        else
        {
         
            DestroyAfterTime();
        }
       // DestroyAfterTime();

     }

   /* private void OnTriggerEnter2D(Collider2D collision)

    {
        if(hitPrefab != null && collision.CompareTag("Enviorment"))
        {
            var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.Euler(0, 0, 0));
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
    */
    private void DestroyAfterTime()
    {
        if(hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.Euler(0, 0, 0));
            bossAi.hasHit = true;
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            
            if (psHit != null)
            {
                Destroy(hitVFX, psHit.main.duration);
            }
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }
       
        Vector3 lastKnownPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        bossAi.gooProjectiles.Add(lastKnownPosition);
       // bossAi.hasHit = true;
        Destroy(gameObject);
    }

    
}
