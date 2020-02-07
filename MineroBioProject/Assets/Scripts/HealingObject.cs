using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour
{
    [SerializeField] private int healingAmount;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.Heal(healingAmount);

        }
    }
}
