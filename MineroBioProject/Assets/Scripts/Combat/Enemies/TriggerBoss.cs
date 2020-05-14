using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used to activate the boss and music on the last level
 */
public class TriggerBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject music;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.SetActive(true);
            music.SetActive(true);
        }
    }
}
