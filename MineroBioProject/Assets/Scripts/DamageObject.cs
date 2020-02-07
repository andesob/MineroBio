using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageObject : MonoBehaviour
{
    public float damageTimeout = 1f;
    private bool canTakeDamage = true;

    [SerializeField] private int damageAmount;
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (canTakeDamage)
        {
            PlayerMovement player = collider.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Damage(damageAmount);


                StartCoroutine(damageTimer());
            }
        }
     
    }

    // A method that returns false until the time has run out. Then returns true.
    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;


    }
}
