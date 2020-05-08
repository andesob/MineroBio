
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnHealth : MonoBehaviour
{
    public int probability;
    public GameObject hearthPrefab; // Stores the heartPrefab
    private Transform Epos; // enemy position


    private void Start()
    {
        Epos = GetComponent<Transform>();
    }

    public void DropItem()
    {
        if (RandomSpawn())
        {
            Instantiate(hearthPrefab, Epos.position, Quaternion.identity);
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

