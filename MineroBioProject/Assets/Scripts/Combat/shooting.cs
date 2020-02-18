using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Transform firePoint; //where the bullet is going to shoot from
    public GameObject bulletPrefab; //The bullet sprite
    private GameObject gun;
    public GameObject player;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject sniperBulletPrefab;

    public float bulletForce = 10f;
    public float speed;

    private AudioSource audioSource;

    private float timeToFire = 0;
    private void Start()
    {
        bulletPrefab = vfx[0];
        sniperBulletPrefab = vfx[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.GetComponent<PlayerPickupScript>().hasGun())
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / sniperBulletPrefab.GetComponent<ProjectileMove>().fireRate;
            ShootSniper();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        audioSource = gun.GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void setGun(GameObject weapon)
    {
        gun = weapon;
    }
    
    public GameObject getGun()
    {
        return gun;
    }
}
    private void ShootSniper()
    {
        if(firePoint != null)
        {   
           GameObject vfx = Instantiate(sniperBulletPrefab, firePoint.transform.position,playerMovement.GetRotation());
           Rigidbody2D rb = vfx.GetComponent<Rigidbody2D>();
           rb.AddForce(firePoint.right * speed, ForceMode2D.Impulse);
           audio.Play();
        }
        }
    }

