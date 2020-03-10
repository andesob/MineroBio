using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 0.1f;
    private float dashSpeed = 0.4f;
    private float dashTime = 0.07f;
    private float dashWaitTime = 1f;

    private string lastDirection;

    private float time;

    private bool shiftPressed;
    private bool canDash;
    private bool isMoving;
    private bool dPressed;
    private bool aPressed;
    private bool wPressed;
    private bool sPressed;
  

    private Rigidbody2D rb;
    private GameObject player;
    private Animator anim;
    private shooting shootingScript;
    private PlayerController playerController;

    public Animator gunAnimation;

    Vector2 movement;


    private void Start()
    {
        player = this.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        shootingScript = player.GetComponent<shooting>();
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
       
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                shiftPressed = true;
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


            if (shiftPressed)
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
                if (shootingScript.GetGun() != null && shootingScript.GetGun().name == "Sniper")
                {
                    gunAnimation.Play("ChangeDirectionX");
                }
            }

            if (sPressed && !isMoving)
            {
                anim.Play("down_walk");
                lastDirection = "S";
                isMoving = true;
                if (shootingScript.GetGun() != null && shootingScript.GetGun().name == "Sniper")
                {
                    gunAnimation.Play("ChangeDirectionXDown");
                }
            }

            if (aPressed && !isMoving)
            {
                anim.Play("left_walk");
                lastDirection = "A";
                isMoving = true;
                if (shootingScript.GetGun() != null && shootingScript.GetGun().name == "Sniper")
                {
                    gunAnimation.Play("ChangeDirectionYLeft");
                }
            }

            if (dPressed && !isMoving)
            {
                anim.Play("right_walk");
                lastDirection = "D";
                isMoving = true;
                if (shootingScript.GetGun() != null && shootingScript.GetGun().name == "Sniper")
                {
                    gunAnimation.Play("ChangeDirectionY");
                }
            }
        }
    }

    private void HandleDash()
    {
        if (shiftPressed)
        {
            if (time + dashTime >= Time.time)
            {
                rb.MovePosition(rb.position + movement * dashSpeed);
            }
            else
            {
                shiftPressed = false;
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
