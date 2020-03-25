using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour

{
    private const int MELEE_DAMAGE = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("TRIGG");
            //collision.GetComponent<BlobAi>().TakeDamage(MELEE_DAMAGE, collision.transform.position - this.transform.position, false);
            collision.GetComponent<EnemyAi>().TakeDamage(MELEE_DAMAGE, collision.transform.position - this.transform.position, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("COLL");
            //collision.gameObject.GetComponent<BlobAi>().TakeDamage(MELEE_DAMAGE, collision.transform.position - this.transform.position, false);
            collision.gameObject.GetComponent<EnemyAi>().TakeDamage(MELEE_DAMAGE, collision.transform.position - this.transform.position, false);
        }
    }
}
