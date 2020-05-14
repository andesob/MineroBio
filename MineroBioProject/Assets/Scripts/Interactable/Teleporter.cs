using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used on the teleporter to teleport a player to another location
 */
public class Teleporter : MonoBehaviour
{
    public GameObject otherTeleporter;
    public GameObject otherDestination;

    private GameObject Player;
    private bool allowTeleport;

    private void Awake() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        allowTeleport = true;
    }

    /*
     * Teleports player on collision
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (allowTeleport)
            {
                StartCoroutine(Teleport());
            }
        }
    }

    IEnumerator Teleport()
    {
        //Gets components needed
        DissolveEffect dissolveEffect = Player.GetComponent<DissolveEffect>();
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        Teleporter teleport = otherTeleporter.GetComponent<Teleporter>();

        dissolveEffect.StartDissolve(2f);
        yield return new WaitForSeconds(1f);

        //Teleports the player
        PlayerController.isGamePaused = true;
        Player.transform.position = new Vector2(otherDestination.transform.position.x, otherDestination.transform.position.y);
        teleport.allowTeleport = false;
        dissolveEffect.StopDissolve(2f);

        yield return new WaitForSeconds(0.35f);
        PlayerController.isGamePaused = false;

        yield return new WaitForSeconds(0.2f);
        teleport.allowTeleport = true;
    }
}
