using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject Player;
    public GameObject Portal;
    public bool allowTeleport=true;

    private void Awake()
    {
        allowTeleport = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collission detected");
    if(collision.gameObject.CompareTag("Player"))
        {
            if (allowTeleport)
            {
                StartCoroutine(Teleport());
            }

        }
    }

   IEnumerator Teleport()
    {
        DissolveEffect dissolveEffect = Player.GetComponent<DissolveEffect>();
        PlayerMovement playerMovement = Player.GetComponent<PlayerMovement>();
        Teleporter teleporter = Portal.GetComponent<Teleporter>();
        dissolveEffect.StartDissolve(2f);
        yield return new WaitForSeconds(1f);

        playerMovement.isInputEnabled = false;
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
       teleporter.allowTeleport = false;
        dissolveEffect.StopDissolve(2f);

        yield return new WaitForSeconds(0.35f);
       playerMovement.isInputEnabled = true;

        yield return new WaitForSeconds(0.2f);
        teleporter.allowTeleport = true;
    }
 

}
