using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private Vector3 mouseDirection;

    public GameObject RightPunch;
    public GameObject LeftPunch;
    public GameObject OverheadPunch;
    public GameObject Kick;
    public float MeleeAttackTimout;
    public float meleeAttackTime;
    public Animator anim;

    private PlayerController playerController;
    private shooting shootingScript;

    private bool canMeeleAttack = true;
    private bool attacking = false;

    private enum State {
        Normal,
        Attacking
    }
    private State state;

    private enum Weapons {Melee, Pistol, Sniper}
    private Weapons weapons;
    

    // Start is called before the first frame update
    void Start()
    {
        playerController = this.gameObject.GetComponent<PlayerController>();
        shootingScript = this.gameObject.GetComponent<shooting>();
        weapons = Weapons.Melee;
        
    }

    // Update is called once per frame
    void Update()
    {
        setWeapon();
        switch (weapons)
        {
            case Weapons.Melee:
                MeleeState();
                Debug.Log("Melee case");
                break;

            case Weapons.Pistol:
                Debug.Log("Pistol case");
                break;

            case Weapons.Sniper:
                Debug.Log("Sniper case");
                break;
        }
    }

    // The melee state
    private void MeleeState()
    {
        switch (state)
        {
            case State.Normal:
                if (Input.GetMouseButtonDown(0))
                {
                    mouseDirection = GetMouseDirection();
                    SetAttackDirection(mouseDirection);
                    Timer(canMeeleAttack, MeleeAttackTimout);
                    Debug.Log(mouseDirection);
                }

                break;

            case State.Attacking:
                Debug.Log("Is attacking");

                break;
        }
    }

    // returns a normalized vector. 
    // [x,y,z]
    // [1,-1,0] = Right
    // [-1,0,0] = Left
    // [0,-1,0] = Down
    // [0,1,0 ] = Up
    private Vector3 GetMouseDirection() { 
    Vector3 mousePosition = MouseUtils.GetMouseWorldPosition();
    Vector3 attackDir = (mousePosition - transform.position).normalized;

    return attackDir;
    }

    private void setWeapon()
    {
        string weaponName = shootingScript.getWeaponName();
        Debug.Log(weaponName);

        if(weaponName == "Pistol")
        {
            weapons = Weapons.Pistol;
        }
        else if (weaponName == "Sniper")
        {
            weapons = Weapons.Sniper;
        }
        else
        {
            weapons = Weapons.Melee;
        }
     

    }
    //Sets the attack direction based on mouseclick. 
    private void SetAttackDirection(Vector3 attackDirection)
    {
        if (canMeeleAttack)
        {
            if (attackDirection.x > 0 && (attackDirection.y > -0.5 && attackDirection.y < 0.5))
            {
                Debug.Log("Attack Right");
                StartCoroutine(SetAttackTimer(RightPunch));  
            }
            if (attackDirection.x < 0 && (attackDirection.y > -0.5 && attackDirection.y < 0.5))
            {
                Debug.Log("Attack Left");
                StartCoroutine(SetAttackTimer(LeftPunch));
            }
            if (attackDirection.y > 0 && (attackDirection.x > -0.5 && attackDirection.x < 0.5))
            {
                Debug.Log("Attack Up");
                StartCoroutine(SetAttackTimer(OverheadPunch));
            }
            if (attackDirection.y < 0 && (attackDirection.x > -0.5 && attackDirection.x < 0.5))
            {
                Debug.Log("Attack Down");
                StartCoroutine(SetAttackTimer(Kick));
            }
        }
    }
      // sets a timer for a boolean value.
    private IEnumerator Timer(bool constraint, float Timeout)
    {
        constraint = false;
        yield return new WaitForSeconds(Timeout);
        constraint = true;
    }
    // sets the melee hitbox active, and puts the character into the attacking state. 
    private IEnumerator SetAttackTimer(GameObject attackSide)
    {
        attackSide.SetActive(true);
        state = State.Attacking;
        yield return new WaitForSeconds(meleeAttackTime);
        attackSide.SetActive(false);
        state = State.Normal;

        // TODO not sure if work as itended. Need to change this. 
        Timer(canMeeleAttack, MeleeAttackTimout);
    }
}
