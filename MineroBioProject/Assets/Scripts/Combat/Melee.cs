using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour

{
    public GameObject attack;
    private PlayerMovement playerMovement;
    private Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.K))
        {
            getRoation();
           var meleeVFX = Instantiate(attack, transform.position, rotation);

            var psMelee = meleeVFX.GetComponent<ParticleSystem>();
            if (psMelee != null)
            {
                Destroy(meleeVFX, psMelee.main.duration);
            }
            else
            {
                var psChild = meleeVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(meleeVFX, psChild.main.duration);
            }
        }
    }

    private void getRoation()
    {
        if(playerMovement.GetLastDirection() == "W")
        {
            rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        if (playerMovement.GetLastDirection() == "A")
        {
            rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        if (playerMovement.GetLastDirection() == "S")
        {
            rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        if (playerMovement.GetLastDirection() == "D")
        {
            rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
