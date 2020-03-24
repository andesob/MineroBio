using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private GameObject RightPunch;
    private GameObject LeftPunch;
    private GameObject OverheadPunch;
    private GameObject Kick;
    private float meleeAttackTimeout = 1f;
    private float meleeAttackTime = 0.5f;
    private float shootingtimeout = 1.5f;
    public Animator anim;

    private PlayerMovement playerMovement;
    private shooting shootingScript;

    private bool canMeeleAttack = true;

    private enum State
    {
        WaitingForInput,
        MeleeAttacking,
        Shooting,
    }
    private State state;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        shootingScript = this.gameObject.GetComponent<shooting>();
        foreach (Transform child in transform)
        {
            GameObject kid = child.gameObject;
            if (kid.name == "RightPunch")
            {
                RightPunch = kid;
            }
            if (kid.name == "LeftPunch")
            {
                LeftPunch = kid;
            }
            if (kid.name == "UpPunch")
            {
                OverheadPunch = kid;
            }
            if (kid.name == "DownKick")
            {
                Kick = kid;
            }
        }
        state = State.WaitingForInput;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.WaitingForInput:
                getInput();
                break;

            case State.MeleeAttacking:

                SetAttackDirection();
                break;

            case State.Shooting:
                break;
        }
    }

    private void getInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            SetAttackDirection();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            shootingScript.Shoot();
            StartCoroutine(SetShootingTimer());
        }
    }

    //Sets the attack direction based on mouseclick. 
    private void SetAttackDirection()
    {
        string lastDirection = playerMovement.GetLastDirection();
        if (canMeeleAttack)
        {
            if (lastDirection == "D")
            {
                StartCoroutine(SetMeleeAttackTimer(RightPunch));
            }
            if (lastDirection == "A")
            {
                StartCoroutine(SetMeleeAttackTimer(LeftPunch));
            }
            if (lastDirection == "W")
            {
                StartCoroutine(SetMeleeAttackTimer(OverheadPunch));
            }
            if (lastDirection == "S")
            {
                StartCoroutine(SetMeleeAttackTimer(Kick));
            }
        }
    }

    // sets the melee hitbox active, and puts the character into the attacking state. 
    private IEnumerator SetMeleeAttackTimer(GameObject attackSide)
    {
        state = State.MeleeAttacking;
        attackSide.SetActive(true);
        canMeeleAttack = false;
        yield return new WaitForSeconds(meleeAttackTime);
        attackSide.SetActive(false);
        canMeeleAttack = true;
        state = State.WaitingForInput;
    }

    // put the player into the shooting state, whichs blocks other inputs while the character is shooting. 
    private IEnumerator SetShootingTimer()
    {
        canMeeleAttack = false;
        state = State.Shooting;
        yield return new WaitForSeconds(meleeAttackTime);
        state = State.WaitingForInput;
        canMeeleAttack = true;
    }
}
