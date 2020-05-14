using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script that starts, stops and show the dialogue UI
 */
public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;

    private Queue<string> sentences;
    private Vector3 dialogueRightPos;
    private Vector3 dialogueLeftPos;

    // Start is called before the first frame update
    void Awake()
    {
        dialogueRightPos = new Vector3(325, 170, 0);
        dialogueLeftPos = new Vector3(-325, 170, 0);
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, string triggerName)
    {
        SetDialoguePos(triggerName);
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }

    /*
     * Sets the visual position of the dialogue box
     */
    private void SetDialoguePos(string triggerName)
    {
        switch (triggerName)
        {
            case "InitDialogueTrigger":
                dialogueBox.transform.localPosition = dialogueRightPos;
                break;

            case "DashDialogueTrigger":
                dialogueBox.transform.localPosition = dialogueLeftPos;
                break;

            case "MeleeDialogueTrigger":
                dialogueBox.transform.localPosition = dialogueLeftPos;
                break;

            case "PistolDialogueTrigger":
                dialogueBox.transform.localPosition = dialogueLeftPos;
                break;

            case "ShootDialogueTrigger":
                dialogueBox.transform.localPosition = dialogueRightPos;
                break;
        }
    }
}
