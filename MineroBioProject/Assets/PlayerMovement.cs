﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float dashSpeed;
    public float dashTime;
    public float dashWaitTime;

    private bool spacePressed;
    private bool canDash = true;
    private float time;

    public Rigidbody2D rb;

    Vector2 movement;



    // Update is called once per frame
    void Update()
    {       
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                canDash = false;
                spacePressed = true;
                time = Time.time;
            }
         HandleDash();
    }

    private void FixedUpdate()
    {
        if (!spacePressed)
        {
            rb.MovePosition(rb.position + movement * moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Powerplant"))
        {
            SceneManager.LoadScene("levelHubScene");
        }
        if (collision.gameObject.CompareTag("PowerplantExit"))
        {
            SceneManager.LoadScene("Default");
        }
    }



    //A method to get the player position. May be used to have a knockback function. 
    // Have not be tested to see if it works yet.

    //public Vector3 GetPosition()
    //{
    //   return GameObject.Find("MainCharacter").transform.position;
    //}

    public void Damage(int damageAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Damage(damageAmount);
    }

    public void Heal(int healingAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Heal(healingAmount);
    }
    private void HandleDash()
    {
        if (spacePressed){

            if (time + dashTime >= Time.time)
            {
                rb.MovePosition(rb.position + movement * dashSpeed);
            }
            else
            {
            spacePressed = false;
            }
        }
        if(time + dashWaitTime <= Time.time && !canDash)
        {
            canDash = true;
        }
    }

    
}
