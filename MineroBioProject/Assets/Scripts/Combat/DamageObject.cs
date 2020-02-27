using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageObject : MonoBehaviour
{
    public float damageTimeout = 1f;
    private bool canTakeDamage = true;

    [SerializeField] private int damageAmount;

    private void Start()
    {
        damageAmount = 1;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (canTakeDamage)
        {
            PlayerController player = collider.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector3 knockbackDir = (player.GetPosition() - transform.position).normalized;
                player.Damage(knockbackDir, 0.6f, damageAmount);
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
