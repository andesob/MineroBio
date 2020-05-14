using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script that handles when the player shoots
 */
public class shooting : MonoBehaviour
{
    public GameObject bulletPrefab; //The bullet sprite
    public GameObject sniperBulletPrefab;

    private PlayerMovement playerMovement;
    private PlayerController playerController;
    private Transform firePoint; //where the bullet is going to shoot from
    private GameObject gun;
    private GameObject player;
    private AudioSource audioSource;
    private List<GameObject> vfx = new List<GameObject>();
    private string weaponName;
    private float bulletForce = 10f;
    private float speed = 20.28f;
    private float timeToFire = 0;


    private void Start()
    {
        player = this.gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
        playerController = player.GetComponent<PlayerController>();
        firePoint = player.transform.GetChild(1);
        if (gun != null)
        {
            audioSource = gun.GetComponent<AudioSource>();
        }
    }

    /*
     * Instantiates the correct bullet and plays the animation and sound
     */
    public void Shoot()
    {
        GameObject bullet;
        Rigidbody2D rb;
        FirePointLocation();

        switch (weaponName)
        {
            case "Pistol":
                if (Time.time >= timeToFire)
                {
                    timeToFire = Time.time + 1 / bulletPrefab.GetComponent<Bullet>().fireRate;
                    bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
                    audioSource = gun.GetComponent<AudioSource>();
                    audioSource.Play();
                }
                break;

            case "Sniper":
                if (Time.time >= timeToFire)
                {
                    timeToFire = Time.time + 1 / sniperBulletPrefab.GetComponent<SniperProjectileMove>().fireRate;
                    bullet = Instantiate(sniperBulletPrefab, firePoint.position, firePoint.rotation);
                    rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.right * speed, ForceMode2D.Impulse);
                    audioSource = gun.GetComponent<AudioSource>();
                    audioSource.Play();
                }
                break;
        }
    }

    /*
     * Sets the location where the bullets will shoot from
     */
    private void FirePointLocation()
    {
        float playerX = playerController.GetPosition().x;
        float playerY = playerController.GetPosition().y;
        switch (playerMovement.GetLastDirection())
        {

            case "W":
                if (weaponName == "Sniper")
                {
                    firePoint.position = new Vector3(playerX + 0.22f, playerY + 0.63f, 0f);
                }
                if (weaponName == "Pistol")
                {
                    firePoint.position = new Vector3(playerX + 0.24f, playerY + 0.18f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case "D":
                if (weaponName == "Sniper")
                {
                    firePoint.position = new Vector3(playerX + 0.68f, playerY + 0, 0f);
                }
                if (weaponName == "Pistol")
                {
                    firePoint.position = new Vector3(playerX + 0.27f, playerY - 0.2f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;

            case "A":
                if (weaponName == "Sniper")
                {
                    firePoint.position = new Vector3(playerX - 0.68f, playerY - 0.05f, 0f);
                }
                if (weaponName == "Pistol")
                {
                    firePoint.position = new Vector3(playerX - 0.27f, playerY - 0.2f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;

            case "S":
                if (weaponName == "Sniper")
                {
                    firePoint.position = new Vector3(playerX + 0.22f, playerY - 0.71f, 0f);
                }
                if (weaponName == "Pistol")
                {
                    firePoint.position = new Vector3(playerX + 0.27f, playerY - 0.98f, 0f);
                }
                firePoint.rotation = Quaternion.Euler(0f, 0f, 270f);
                break;
        }
    }

    public void SetGun(GameObject weapon)
    {
        gun = weapon;
        weaponName = weapon.name;
    }

    public GameObject GetGun()
    {
        return gun;
    }

    public string GetWeaponName()
    {
        return weaponName;
    }

    public Transform GetFirepoint()
    {
        return firePoint;
    }

    public List<GameObject> GetVFX()
    {
        return vfx;
    }
}