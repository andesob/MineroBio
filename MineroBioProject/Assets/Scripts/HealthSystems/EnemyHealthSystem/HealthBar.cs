using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }


    // istendefor å update hele tiden så updater den bare når man tar damage eller healer, virkelig ikke spør meg hvordan dette fungerer men med godvilja skal jeg klare å forklare :)
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.getHealthPercent(), 1);
    }
}
