using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Transform firePoint; //where the bullet is going to shoot from
   
    private GameObject bulletPrefab; //The bullet sprite
    private GameObject sniperBulletPrefab;

    public Transform gun;
    public GameObject player;

    public List<GameObject> vfx = new List<GameObject>();

    public float bulletForce = 10f;
    public float speed;

    private float timeToFire = 0;

    private AudioSource audio;
    private void Start()
    {
        bulletPrefab = vfx[0];
        sniperBulletPrefab = vfx[1];
        audio = gun.GetComponent<AudioSource>();
        
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
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
        audio.Play();
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

