using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script for damaging enemies with melee.
 * If the melee prefab collides with an enemy it does damage.
 */
public class Melee : MonoBehaviour
{
    private const int MELEE_DAMAGE = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
        { 
            Vector3 characterPosition = GameObject.Find("Main Char prefab 1/MainCharacter").GetComponent<Transform>().position;

            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyAi>().TakeDamage(MELEE_DAMAGE, collision.transform.position - characterPosition, true);
            }

            if (collision.CompareTag("Boss"))
            {
                collision.GetComponent<BossAI>().TakeDamage(MELEE_DAMAGE, collision.transform.position - characterPosition, true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyAi>().TakeDamage(MELEE_DAMAGE, collision.transform.position - this.transform.position, true);
            }

            if (collision.gameObject.CompareTag("Boss"))
            {
                collision.gameObject.GetComponent<BossAI>().TakeDamage(MELEE_DAMAGE, collision.transform.position - this.transform.position, true);
            }
        }
    }
