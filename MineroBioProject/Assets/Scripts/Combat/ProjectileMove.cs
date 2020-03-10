using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{

    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
  
    

    // Start is called before the first frame update
    void Start()
    {
     
        if (muzzlePrefab != null)
        {

            var muzzleVFX = Instantiate(muzzlePrefab, transform.position,transform.rotation);
         
            var psMuzzle = muzzleVFX.GetComponent<ParticleSystem>();
            if (psMuzzle != null)
            {
                Destroy(muzzleVFX, psMuzzle.main.duration);
            }
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzzleVFX, psChild.main.duration);
                    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(speed!=0)
        {
            
           
            
        }
        else
        {
            Debug.Log("KUKK");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0;
        
        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
        Vector2 pos = contact.point;

        if(hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
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

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
