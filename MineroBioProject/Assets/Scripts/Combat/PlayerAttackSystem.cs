using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private GameObject RightPunch;
    private GameObject LeftPunch;
    private GameObject OverheadPunch;
    private GameObject Kick;
    private float MeleeAttackTimout = 1f;
    private float meleeAttackTime = 0.5f;
    public Animator anim;

    private PlayerMovement playerMovement;
    private shooting shootingScript;

    private bool canMeeleAttack = true;

    private enum State {
        WaitingForInput,
        MeleeAttacking,
        Shooting,
    }
    private State state;
    private string weaponName;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
        shootingScript = this.gameObject.GetComponent<shooting>();
        foreach(Transform child in transform)
        {
            GameObject kid = child.gameObject;
            if(kid.name == "RightPunch")
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
        SetWeapon();
        
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
                Debug.Log("Shooting state reached");
                //SetWeapon();
                break;
        }
    }

    private void SetWeapon()
    {
        weaponName = shootingScript.GetWeaponName();
    }
    //Sets the attack direction based on mouseclick. 
    private void SetAttackDirection()
    {
        string lastDirection = playerMovement.GetLastDirection();
        if (canMeeleAttack)
        {
            if (lastDirection == "D")
            {
                StartCoroutine(SetAttackTimer(RightPunch));  
            }
            if (lastDirection == "A")
            {
                StartCoroutine(SetAttackTimer(LeftPunch));
            }
            if (lastDirection == "W")
            {      
                StartCoroutine(SetAttackTimer(OverheadPunch));
            }
            if (lastDirection == "S")
            {
                StartCoroutine(SetAttackTimer(Kick));
            }
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
            SetWeapon();
            Debug.Log("Shooting" + weaponName);
            shootingScript.Shoot(weaponName);
            }


    }
    // sets the melee hitbox active, and puts the character into the attacking state. 
    private IEnumerator SetAttackTimer(GameObject attackSide)
    {
        attackSide.SetActive(true);
        canMeeleAttack = false;
        yield return new WaitForSeconds(meleeAttackTime);
        attackSide.SetActive(false);
        canMeeleAttack = true;
        state = State.WaitingForInput;

        // TODO not sure if work as itended. Need to change this. 
       // Timer(canMeeleAttack, MeleeAttackTimout);
    }
    /*
    private IEnumerator SetShootingStateTimer(float ShootingTimout)
    {
        state = State.Shooting;
        yield return new WaitForSeconds(ShootingTimout);
        state = State.WaitingForInput;
    }
    */
}
