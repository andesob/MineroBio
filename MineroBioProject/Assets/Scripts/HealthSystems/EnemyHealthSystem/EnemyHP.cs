using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public Transform pfHealthBar;

    void Start()
    {
        HealthSystem healthSystem = new HealthSystem(100);

        Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(0.5f, 1), Quaternion.identity);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
