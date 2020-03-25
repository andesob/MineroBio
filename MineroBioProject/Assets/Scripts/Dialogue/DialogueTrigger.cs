using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void TriggerDialogue(string triggerName)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, triggerName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Melee"))
        {
            TriggerDialogue(this.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("Melee"))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}
