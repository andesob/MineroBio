using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that visually change the healthbar when an enemy takes damage
 */
public class HealthBar : MonoBehaviour
{
    public HealthSystem healthSystem;
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;

        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    //Istendefor å oppdatere hele tiden så updater den bare når man tar damage eller healer
    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new Vector3(healthSystem.getHealthPercent(), 1);
    }
}
