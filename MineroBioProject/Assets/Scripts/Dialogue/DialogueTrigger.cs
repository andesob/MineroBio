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
        if(this.name == "InitDialogueTrigger")
        {
            TriggerDialogue(this.name);
        }
    }

    public void TriggerDialogue(string triggerName)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, triggerName);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Melee") && collision.CompareTag("Player"))
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
