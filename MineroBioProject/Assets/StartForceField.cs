using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            //ForceField forcefield = collider.GetComponent<ForceField>();

            Instantiate(forceFieldPref, player.transform.position, player.transform.rotation);


            Destroy(this.gameObject);
        }
    }
}
