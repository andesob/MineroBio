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

    private bool spacePressed;
    private bool canDash = true;
    private float time;
    private string lastDirection = "down";
    private bool isMoving;
    private float playerY;
    private float playerX;

    private bool dPressed;
    private bool aPressed;
    private bool wPressed;
    private bool sPressed;


    public Rigidbody2D rb;
    public Animator anim;
    public shooting shootingScript;

    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
         movement.x = Input.GetAxisRaw("Horizontal");
         movement.y = Input.GetAxisRaw("Vertical");

        getInput();
        HandleKeyPress();
        HandleDash();

        if (!Input.inputString.Equals(lastDirection)){
            isMoving = false;
        }
    }

    private void FixedUpdate()
    {
        if (!spacePressed)
        {
            rb.MovePosition(rb.position + movement * moveSpeed);
        }
    }


    //Gets the input from a user and sets a boolean to true or false accordingly.
    private void getInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            canDash = false;
            spacePressed = true;
            time = Time.time;
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
            if (lastDirection.Equals("W"))
            {
                anim.Play("idle_up");
            } 
            else if (lastDirection.Equals("S"))
            {
                anim.Play("idle_down");
            } 
            else if (lastDirection.Equals("A"))
            {
                anim.Play("idle_left");
            } 
            else if (lastDirection.Equals("D"))
            {
                anim.Play("idle_right");
            }
        }
    }

    private void HandleKeyPress()
    {
        if(wPressed && !isMoving)
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
                shootingScript.firePoint.transform.position = new Vector3(playerX - 0.1f, playerY - 0.2f, 0f);
                break;

            case "D":
                shootingScript.firePoint.rotation = Quaternion.Euler(0f, 0f, -90f);
                shootingScript.firePoint.transform.position = new Vector3(playerX + 0.1f, playerY - 0.2f, 0f);
                break;

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
            transform.position = new Vector3(0.53f, 22.5f);
        }
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
