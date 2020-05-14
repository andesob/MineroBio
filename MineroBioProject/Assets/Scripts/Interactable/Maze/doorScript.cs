using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script used on the doors to open and close them
 */
public class doorScript : MonoBehaviour
{
    [SerializeField] GameObject doorOpen;
    [SerializeField] GameObject doorClosed;

    private BoxCollider2D boxCollider;

    public bool isOpen;

    // sets the state of the door according to what the isOpen bool says.
    private void Start()
    {
        if (isOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen.GetComponent<SpriteRenderer>().sprite;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
        else if (!isOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed.GetComponent<SpriteRenderer>().sprite;
            boxCollider = gameObject.GetComponent<BoxCollider2D>();
        }
    }

    // changes the state of the door.
    public void changeState()
    {
        if (isOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed.GetComponent<SpriteRenderer>().sprite;
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
            isOpen = false;
        }
        else if (!isOpen)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen.GetComponent<SpriteRenderer>().sprite;
            Destroy(boxCollider);
            isOpen = true;
        }
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen.GetComponent<SpriteRenderer>().sprite;
        Destroy(boxCollider);
    }
}
