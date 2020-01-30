using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    private float dissolveAmount;
    private bool isDissolving;

    // Update is called once per frame
    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        } else
        {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - Time.deltaTime);
            material.SetFloat("_DissolveAmount", dissolveAmount);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isDissolving = true;
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            isDissolving = false;
        }
    }
}
