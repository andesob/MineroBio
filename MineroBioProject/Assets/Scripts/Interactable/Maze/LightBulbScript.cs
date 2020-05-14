using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/*
 * Script used to turn the lightbulbs on and off 
 */
public class LightBulbScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject switchObject;

    private bool isOn;

    void Start()
    {
        isOn = switchObject.GetComponent<Switch>().getIsOn();        
    }

    // Update is called once per frame
    void Update()
    {
        isOn = switchObject.GetComponent<Switch>().getIsOn();
        if (isOn)
        {
            this.GetComponent<Light2D>().intensity = 1;
        }
        else
        {
            this.GetComponent<Light2D>().intensity = 0;
        }
    }
}
