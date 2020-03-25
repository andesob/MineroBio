using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private GameObject RightPunch;
    private GameObject LeftPunch;
    private GameObject OverheadPunch;
    private GameObject Kick;
    public GameObject attack;
    private float meleeAttackTimeout = 1f;
    private float meleeAttackTime = 0.5f;
    private float shootingtimeout = 1.5f;

    private Quaternion rotation;

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

                break;

            case State.Shooting:
                break;
        }
    }

    private void getInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            getRoation();
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            shootingScript.Shoot();
            StartCoroutine(SetShootingTimer());
        }
    }

    private void Attack()
    {
        var meleeVFX = Instantiate(attack, transform.position, rotation);

        var psMelee = meleeVFX.GetComponent<ParticleSystem>();
        if (psMelee != null)
        {
            Destroy(meleeVFX, psMelee.main.duration);
        }
        else
        {
            var psChild = meleeVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(meleeVFX, psChild.main.duration);
        }
    }

    private void getRoation()
    {
        if (playerMovement.GetLastDirection() == "W")
        {
            rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if (playerMovement.GetLastDirection() == "A")
        {
            rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (playerMovement.GetLastDirection() == "S")
        {
            rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        if (playerMovement.GetLastDirection() == "D")
        {
            rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        StartCoroutine(SetMeleeAttackTimer());
    }

    // sets the melee hitbox active, and puts the character into the attacking state. 
    private IEnumerator SetMeleeAttackTimer()
    {
        state = State.MeleeAttacking;
        canMeeleAttack = false;
        yield return new WaitForSeconds(meleeAttackTime);
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
