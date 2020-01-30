using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public GameObject Player;
    public GameObject Portal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collission detected");
    if(collision.gameObject.CompareTag("Player"))
        {
           
            StartCoroutine(Teleport());
            

        }
    }

   IEnumerator Teleport()
    {
        DissolveEffect dissolveEffect = Player.GetComponent<DissolveEffect>();
        dissolveEffect.StartDissolve(2f);
        yield return new WaitForSeconds(1f);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);
        dissolveEffect.StopDissolve(2f);

    }
 

}
