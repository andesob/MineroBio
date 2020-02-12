using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    private float dissolveAmount;
    private float dissolveSpeed;
    private bool isDissolving;

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp(dissolveAmount + dissolveSpeed*Time.deltaTime,0,1.2f);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        } else
        {
            dissolveAmount = Mathf.Clamp(dissolveAmount - dissolveSpeed*Time.deltaTime, 0, 1.2f);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }
    public void StartDissolve(float dissolveSpeed)
        { 
        isDissolving = true;
        this.dissolveSpeed = dissolveSpeed;
        }

     public void StopDissolve(float dissolveSpeed)
        {
            isDissolving = false; 
            this.dissolveSpeed = dissolveSpeed;
        }
    
}
