using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private int damageAmount;

    private void Start()
    {
        damageAmount = 1;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.Damage(damageAmount);
        }
    }
}
