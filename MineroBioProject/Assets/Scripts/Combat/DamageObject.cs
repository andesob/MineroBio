﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script that can be added to an object that is supposed to do damage on the player
 */
public class DamageObject : MonoBehaviour
{
    public float damageTimeout;
    public float knockbackDistance;

    private ForceField forceField;
    private bool canDamage = true;
    [SerializeField] private int damageAmount;


    private void Start()
    {
        damageAmount = 1;
    }

    /*
     * When the gameobject collides with the player do damage on the player
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") && collider.isTrigger && canDamage)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            GameObject ForceFieldObject = GameObject.FindGameObjectWithTag("ForceField");
            if (ForceFieldObject != null)
            {
                forceField = GameObject.FindGameObjectWithTag("ForceField").GetComponent<ForceField>(); ;
            }
            if (player != null && forceField != null && forceField.getShieldHealth() > 0 && canDamage)
            {
                forceField.damageShield();
                StartCoroutine(damageTimer(damageTimeout));
            }
            else
            {
                Vector2 direction = collider.transform.position - transform.position;
                Vector2 thrust = direction.normalized * knockbackDistance;

                player.Damage(thrust, damageAmount);
                StartCoroutine(damageTimer(damageTimeout));
            }
        }
    }

    // A method that returns false until the time has run out. Then returns true.
    private IEnumerator damageTimer(float timeout)
    {    
        canDamage = false;
        yield return new WaitForSeconds(timeout);
        canDamage = true;
    }

    public bool GetCanDamage()
    {
        return canDamage;
    }
}
