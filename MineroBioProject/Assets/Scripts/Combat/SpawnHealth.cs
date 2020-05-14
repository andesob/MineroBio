
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used to spawn health pickups when an enemy is killed
 */
public class SpawnHealth : MonoBehaviour
{
    public GameObject hearthPrefab; // Stores the heartPrefab
    public int probability;

    private Vector3 enemyPosition; // enemy position

    private void Start()
    {
        enemyPosition = GetComponent<Transform>().position;
    }

    public void DropItem()
    {
        if (RandomSpawn())
        {
            Instantiate(hearthPrefab, enemyPosition, Quaternion.identity);
        }
    }

    private bool RandomSpawn()
    {
        int randomNumber = Random.Range(1, 100);

        if (randomNumber <= probability)
        {
            return true;
        }
        return false;
    }
}

