﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that heals the player on collision
 */
public class HealingObject : MonoBehaviour
{
    public int healingAmount;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)

        {
            PlayerController player = collider.GetComponent<PlayerController>();
            player.Heal(healingAmount);
            Destroy(this.gameObject);
        }
    }
}
