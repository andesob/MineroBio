using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject RightPunch;
    public GameObject LeftPunch;
    public GameObject OverheadPunch;
    public GameObject Kick;
    public float MeleeAttackTimout;
    public float meleeAttackTime;
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
        weaponName = shootingScript.getWeaponName();
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
