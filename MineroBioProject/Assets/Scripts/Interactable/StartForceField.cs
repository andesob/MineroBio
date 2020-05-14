using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that instantiates the forcefield around the player after it is picked up
 */
public class StartForceField : MonoBehaviour
{
    public GameObject forceFieldPref;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.isTrigger)
        {
            Instantiate(forceFieldPref, player.transform.position, player.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
