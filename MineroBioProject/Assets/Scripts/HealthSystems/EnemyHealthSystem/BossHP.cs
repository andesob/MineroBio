using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Sets the healtpoints of the boss. Extends EnemyHP
 */
public class BossHP : EnemyHP
{
    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(1337);
        healthBar.Setup(healthSystem);
    }
}
