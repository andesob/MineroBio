using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used to spawn forcefield when an enemy is killed
 */
public class SpawnForceField : MonoBehaviour
{
    public int probability;
    public GameObject forceFieldSprite; // Stores the forcefieldSprite

    private Transform Epos; // enemy position


    private void Start()
    {
        Epos = GetComponent<Transform>();
    }

    public void DropItem()
    {
        if (RandomSpawn())
        {
            Instantiate(forceFieldSprite, Epos.position, Quaternion.identity);
        }
    }

    private bool RandomSpawn()
    {
        bool canSpawn = false;
        int randomNumber = Random.Range(1, 100);

        if (randomNumber <= probability)
        {
            canSpawn = true;
        }
        return canSpawn;
    }
}




