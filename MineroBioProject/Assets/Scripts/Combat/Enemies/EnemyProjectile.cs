using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float fireRate = 2;

    public GameObject hitPrefab;
    public GameObject muzzlePrefab;
    public GameObject tip;
    public GameObject player;
    private float time;

    public Rigidbody2D rb;
    void Start()
    {
       // rb = GetComponent<Rigidbody2D>();
        if (muzzlePrefab != null)
        {
           
            var muzzleVFX = Instantiate(muzzlePrefab, transform.position, transform.rotation);

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

    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Bullet") && Time.time > time)
        {
            time = Time.time + 1;
            //ContactPoint2D contact = collision.contacts[0];
            //Quaternion rot = Quaternion.FromToRotation(Vector2.up, contact.normal);
            //Vector2 pos = contact.point;
            Vector2 pos = tip.transform.position;
            Quaternion rot = tip.transform.rotation;

            if (hitPrefab != null)
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Enemy") && !collision.CompareTag("Bullet") && Time.time > time)
        {

            time = Time.time + 1;
            Vector2 pos = tip.transform.position;
            Quaternion rot = Quaternion.Euler(tip.transform.rotation.x, tip.transform.rotation.y, tip.transform.rotation.z + 90f);

            if (hitPrefab != null)
            {
                var hitVFX = Instantiate(hitPrefab, pos, rot);
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
            Destroy(gameObject);
        }
    }
}
