﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private readonly float moveSpeed = 0.1f;
    private readonly float dashSpeed = 0.4f;
    private readonly float dashTime = 0.07f;
    private readonly float dashWaitTime = 1f;

    private string lastDirection;

    private float time;

    private bool spacePressed;
    private bool canDash;
    private bool isMoving;
    private bool dPressed;
    private bool aPressed;
    private bool wPressed;
    private bool sPressed;

    private Rigidbody2D rb;
    private GameObject player;
    private Animator anim;
    private PlayerController playerController;

    Vector2 movement;

    private void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        anim = player.GetComponent<Animator>();
        canDash = true;
        lastDirection = "S";
        playerController = this.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.CanMove())
        {
            getInput();
            HandleKeyPress();
            HandleDash();
        }
    }


    //Gets the input from a user and sets a boolean to true or false accordingly.
    private void getInput()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            spacePressed = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            dPressed = true;
        }
        else
        {
            dPressed = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            aPressed = true;
        }
        else
        {
            aPressed = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            sPressed = true;
        }
        else
        {
            sPressed = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            wPressed = true;
        }
        else
        {
            wPressed = false;
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            switch (lastDirection)
            {
                case "W":
                    anim.Play("idle_up");
                    break;

                case "S":
                    anim.Play("idle_down");
                    break;

                case "A":
                    anim.Play("idle_left");
                    break;

                case "D":
                    anim.Play("idle_right");
                    break;
            }
        }

        if (!Input.inputString.Equals(lastDirection))
        {
            isMoving = false;
        }
    }


    private void HandleKeyPress()
    {
        if (!PlayerController.isGamePaused)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            playerController.RotateGun(lastDirection);

            if (spacePressed)
            {
                if (canDash)
                {
                    canDash = false;
                    time = Time.time;
                }
            }
            else
            {
                rb.MovePosition(rb.position + movement * moveSpeed);
            }

            if (wPressed && !isMoving)
            {
                anim.Play("up_walk");
                lastDirection = "W";
                isMoving = true;
            }

            if (sPressed && !isMoving)
            {
                anim.Play("down_walk");
                lastDirection = "S";
                isMoving = true;
            }

            if (aPressed && !isMoving)
            {
                anim.Play("left_walk");
                lastDirection = "A";
                isMoving = true;
            }

            if (dPressed && !isMoving)
            {
                anim.Play("right_walk");
                lastDirection = "D";
                isMoving = true;
            }
        }
    }

    private void HandleDash()
    {
        if (spacePressed)
        {
            if (time + dashTime >= Time.time)
            {
                rb.MovePosition(rb.position + movement * dashSpeed);
            }
            else
            {
                spacePressed = false;
            }
        }
        if (time + dashWaitTime <= Time.time && !canDash)
        {
            canDash = true;
        }
    }

    public string GetLastDirection()
    {
        return lastDirection;
    }
}
