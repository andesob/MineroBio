using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnForceField : MonoBehaviour
{
    // Start is called before the first frame update

   // public GameObject ForceField_prefab;
    public GameObject ForceFieldPrefab;
    
  
    

    // Update is called once per frame
    void Update()
    {

        spawnForceField();
    }
  
    void spawnForceField()
    {
        
        if (Input.GetKeyDown(KeyCode.J))
        {
           
           GameObject ForceField = Instantiate(ForceFieldPrefab);
        } 
    }
  
}
  
