using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float dashSpeed;
    public float dashTime;
    public float dashWaitTime;

    public bool isInputEnabled = true;

    private string lastDirection = "S";

    private float time;
    private float playerY;
    private float playerX;

    private bool shiftPressed;
    private bool canDash = true;
    private bool isMoving;
    private bool dPressed;
    private bool aPressed;
    private bool wPressed;
    private bool sPressed;


    public Rigidbody2D rb;
    public Animator anim;
    public shooting shootingScript;

    Vector2 movement;


    private void Start()
    {
        isInputEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        HandleKeyPress();
        HandleDash();
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
        if (isInputEnabled)
        {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        }

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
            Walk(lastDirection);
        }

        if (sPressed && !isMoving)
        {

            anim.Play("down_walk");
            lastDirection = "S";
            isMoving = true;
            Walk(lastDirection);
        }

        if (aPressed && !isMoving)
        {

            anim.Play("left_walk");
            lastDirection = "A";
            isMoving = true;
            Walk(lastDirection);
        }

        if (dPressed && !isMoving)
        {

            anim.Play("right_walk");
            lastDirection = "D";
            isMoving = true;
            Walk(lastDirection);
        }
    }

    private void Walk(string direction)
    {
        playerX = this.gameObject.transform.position.x;
        playerY = this.gameObject.transform.position.y;

        switch (direction)
        {
            case "W":
                shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, 0f);
                shootingScript.firePoint.transform.position = new Vector3(playerX + 0.2f, playerY, 0f);
                break;

            case "S":
                shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, 180f);
                shootingScript.firePoint.transform.position = new Vector3(playerX - 0.25f, playerY, 0f);
                break;

            case "A":
                shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, 90f);
                shootingScript.gun.rotation = Quaternion.Euler(0f, 180f, 0f);
                shootingScript.firePoint.transform.position = new Vector3(playerX - 0.1f, playerY - 0.16f, 0f);
                break;

            case "D":
                shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, -90f);
                shootingScript.gun.rotation = Quaternion.Euler(0f, 0f, 0f);
                shootingScript.firePoint.transform.position = new Vector3(playerX + 0.1f, playerY - 0.13f, 0f);
                break;
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


    public void Damage(int damageAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Damage(damageAmount);
    }

    public void Heal(int damageAmount)
    {
        HeartsHealthVisual2.heartsHealthSystemStatic.Heal(damageAmount);
    }
}
