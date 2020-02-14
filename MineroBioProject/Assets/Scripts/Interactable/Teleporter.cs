using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject Player;
    public GameObject Portal;
    public GameObject Destination;
    public bool allowTeleport = true;

    private void Awake() 
    {
        allowTeleport = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collission detected");
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
        Teleporter teleport = Portal.GetComponent<Teleporter>();
        dissolveEffect.StartDissolve(2f);
        yield return new WaitForSeconds(1f);

        PlayerController.isGamePaused = true;
        Player.transform.position = new Vector2(Destination.transform.position.x, Destination.transform.position.y);
        teleport.allowTeleport = false;
        dissolveEffect.StopDissolve(2f);

        yield return new WaitForSeconds(0.35f);
        PlayerController.isGamePaused = false;

        yield return new WaitForSeconds(0.2f);
        teleport.allowTeleport = true;
    }
}
