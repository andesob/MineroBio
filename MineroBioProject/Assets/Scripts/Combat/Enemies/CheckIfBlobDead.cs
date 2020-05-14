using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script checks if the blob in the tutorial level is dead and if the audio clip is done
 */
public class CheckIfBlobDead : MonoBehaviour
{
    
    public GameObject blob;
    private bool opened = false;
    public bool clipFinished = false;

    // Update is called once per frame
    void Update()
    {
        if (blob == null && !opened && clipFinished)
        {
            GetComponent<doorScript>().changeState();
            opened = true;
        }
    }
}
