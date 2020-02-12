﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint; //where the bullet is going to shoot from
    public GameObject bulletPrefab; //The bullet sprite
    public Transform gun;
    public GameObject player;

    public float bulletForce = 10f;

    private AudioSource audio;

    private void Start()
    {
        audio = gun.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.GetComponent<PlayerPickupScript>().hasGun())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        audio.Play();
    }
}
