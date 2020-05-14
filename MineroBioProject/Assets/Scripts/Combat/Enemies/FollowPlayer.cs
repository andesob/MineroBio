using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Makes the feathers that the boss shoot stick to the player
 */
public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform feather;
    private float startTime;

    private void Awake()
    {
        startTime = Time.time+0.1f;
        feather = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            feather.transform.position = player.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Time.time < startTime)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
}


