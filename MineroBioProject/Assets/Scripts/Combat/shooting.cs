using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public float X = 0;
    public float Y = 0;

    public PlayerMovement playerMovement;
    public PlayerPickupScript playerPickupScript;

    public Transform firePoint; //where the bullet is going to shoot from
    private GameObject bulletPrefab; //The bullet sprite
    private GameObject gun;
    public GameObject player;
    public List<GameObject> vfx = new List<GameObject>();
    private GameObject sniperBulletPrefab;
    private string weaponName;
    public float bulletForce = 10f;
    public float speed;

    private AudioSource audioSource;

    private float timeToFire = 0;
    private void Start()
    {
        bulletPrefab = vfx[0];
        sniperBulletPrefab = vfx[1];
        if (gun != null)
        {
            audioSource = gun.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        FirePointLocation();
        if (Input.GetKeyDown(KeyCode.E) /*&& Time.time >= timeToFire && player.GetComponent<PlayerPickupScript>().hasGun()*/)
        {
            timeToFire = Time.time + 1 / sniperBulletPrefab.GetComponent<ProjectileMove>().fireRate;
            
            Shoot(weaponName);
        }

    }

    private void Shoot(string weapon)
    {
        
        switch (weapon)
        {
            case "Pistol":
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
                audioSource = gun.GetComponent<AudioSource>();
                audioSource.Play();
                break;
            case "Sniper":
                
                bullet = Instantiate(sniperBulletPrefab, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.right * speed, ForceMode2D.Impulse);
                audioSource = gun.GetComponent<AudioSource>();
                audioSource.Play();
                print(gun.name);
                break;
        }

      
    }

    public void FirePointLocation()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        switch (playerMovement.GetLastDirection())
        {
           
            case "W":
                if (playerPickupScript.hasSniper())
                {
                    firePoint.position = new Vector3( playerX+0.22f, playerY + 0.63f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case "D":
                if (playerPickupScript.hasSniper())
                {
                    firePoint.position = new Vector3(playerX + 0.68f, playerY + 0, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;

            case "A":
                if (playerPickupScript.hasSniper())
                {
                    firePoint.position = new Vector3(playerX - 0.68f, playerY - 0.05f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f,0f,180f);
                break;

            case "S":
                if (playerPickupScript.hasSniper())
                {
                    firePoint.position = new Vector3(playerX + 0.22f, playerY - 0.71f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 270f);
                break;
        }
    }

    public void setGun(GameObject weapon)
    {
        gun = weapon;
        weaponName = weapon.name;
    }

    public GameObject getGun()
    {
        return gun;
    }


  
}
  
