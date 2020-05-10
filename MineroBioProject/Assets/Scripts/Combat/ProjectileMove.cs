using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{

    public float speed;
    public float fireRate;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public GameObject tip;

    private float time;

    // Start is called before the first frame update
    void Start()
    {

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
        if (!(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Melee")) && Time.time > time)
        {
            BulletHit(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.CompareTag("Player") || collision.CompareTag("Melee")) && Time.time > time)
        {
            BulletHit(collision.gameObject);
        }
    }


    private void BulletHit(GameObject collision)
    {
        speed = 0;

        Quaternion rot = Quaternion.Euler(tip.transform.rotation.x, tip.transform.rotation.y, tip.transform.rotation.z + 90f);
        Vector2 pos = tip.transform.position;

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

            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Boss"))
            {
                collision.GetComponent<EnemyAi>().TakeDamage(100, collision.transform.position - this.transform.position, false);
            }

            Destroy(gameObject);
        }
    }
}
