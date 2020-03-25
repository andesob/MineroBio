using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fireRate = 2;
    public GameObject hitPrefab;
    public GameObject muzzlePrefab;
    public GameObject tip;

    private float time;
    private int PISTOL_DAMAGE = 50;

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

            var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitVFX, psChild.main.duration);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyAi>().TakeDamage(PISTOL_DAMAGE);
        }
        Destroy(gameObject);
    }
}
