using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private GameObject player;
    public AudioSource source;
    public AudioClip[] clips;
    private bool playClip;

    public void Start()
    {
        playClip = true;
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
            print("i enterededed");
            PlayAudio();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Melee"))
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

    private void PlayAudio()
    {
        foreach (AudioClip clip in clips)
        {
            Time.timeScale = 0f;
            
            if (playClip) {
                playClip = false;
                print(playClip);
                StartCoroutine(Waiter(clip));
            }
        }
        Time.timeScale = 1f;
        print("HEROOO");
    }

    private IEnumerator Waiter(AudioClip clip)
    {
        source.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        playClip = true;
    }
}
