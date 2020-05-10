using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;
    public AudioSource source;
    public AudioClip clip;
    private bool audioPlayed = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
            if (!audioPlayed)
            {
                StartCoroutine(Waiter(clip));
                audioPlayed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Melee"))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    private IEnumerator Waiter(AudioClip clip)
    {
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);

        if (gameObject.name == "InitDialogueTrigger")
        {
            GameObject.Find("Blockades/Block1").SetActive(false);
        }
        else if (gameObject.name == "DashDialogueTrigger")
        {
            GameObject.Find("Blockades/blockadeWide").SetActive(false);
        }
        else if (gameObject.name == "MeleeDialogueTrigger")
        {
            GameObject.Find("Blockades/GreenDoor").SetActive(false);
        }
        else if (gameObject.name == "PistolDialogueTrigger")
        {
            GameObject.Find("Blockades/blockadeUltraWide").SetActive(false);
        }
    }
}