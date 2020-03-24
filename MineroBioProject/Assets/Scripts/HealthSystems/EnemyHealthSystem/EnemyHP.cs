using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public HealthBar healthBar;

    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);
        healthBar.Setup(healthSystem);
    }
}
