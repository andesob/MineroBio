using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfBlobDead : MonoBehaviour
{

    public GameObject blob;
    private bool opened = false;

    // Update is called once per frame
    void Update()
    {
        if (blob == null && !opened)
        {
            GetComponent<doorScript>().changeState();
            opened = true;
        }
    }
}
