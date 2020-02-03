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
    public Animator anim;

    private bool spacePressed;
    private bool canDash = true;
    private float time;
    private string lastDirection = "down";
    private bool isMoving;
    private bool lastInput;

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
        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            anim.Play("right_walk");
            lastDirection = "D";
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A) && !isMoving)
        {
            anim.Play("left_walk");
            lastDirection = "A";
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            anim.Play("down_walk");
            lastDirection = "S";
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.W) && !isMoving)
        {
            anim.Play("up_walk");
            lastDirection = "W";
            isMoving = true;
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
