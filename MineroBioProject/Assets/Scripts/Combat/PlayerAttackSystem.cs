using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public GameObject attack;
    private float meleeAttackTime = 0.5f;
    private float shootingtimeout = 0.5f;

    private Quaternion rotation;

    //public Animator anim;

    private PlayerMovement playerMovement;
    private PlayerController playerController;
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
        playerController = this.gameObject.GetComponent<PlayerController>();
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
            StartCoroutine(SetMeleeAttackTimer());
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
        //playerController.setCanMove(false);  // NEED TO FIX THE WALKING ANIMATION WHILE PAUSING THE MOVEMENT 
        yield return new WaitForSeconds(meleeAttackTime);
        canMeeleAttack = true;
        //playerController.setCanMove(true);
        state = State.WaitingForInput;
    }

    // put the player into the shooting state, whichs blocks other inputs while the character is shooting. 
    private IEnumerator SetShootingTimer()
    {
        canMeeleAttack = false;
        state = State.Shooting;
        yield return new WaitForSeconds(shootingtimeout);
        state = State.WaitingForInput;
        canMeeleAttack = true;

    }
}
