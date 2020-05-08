using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Transform feather;
    private Vector3 originalPos;
    private float startTime;
    private bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        //hasCollided = false;
    }
    private void Awake()
    {
        hasCollided = false;
        startTime = Time.time+0.1f;
        feather = transform.GetChild(0);
        originalPos = feather.transform.position;
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
        if (collision.CompareTag("Player")&&Time.time<startTime)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }



    }
}


