using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A script for the forcefield object that can be picked up
 * Makes the shield follow the player and play animations when damaged
 */
public class ForceField : MonoBehaviour
{
    private GameObject Player;
    private Animation anim;
    private int shieldHealth;

 
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GameObject.FindGameObjectWithTag("ForceField").GetComponent<Animation>();

        shieldHealth = 2;
    }

    // Update is called once per frame
    void Update()
    {  
        FollowPlayer();
    }

    void FollowPlayer()
    {
        float x = Player.transform.position.x;
        float y = Player.transform.position.y;

        transform.position = new Vector2(x, y);
    }
       
   IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds (0.4f);
        Destroy(gameObject);
    }

    public void damageShield()
    {
        if (shieldHealth == 2)

        {
            shieldHealth -= 1;
            anim.Play("ff_dmg");
        }
        else if (shieldHealth == 1)
        {
            anim.Play("ff_destroy");
            StartCoroutine(WaitToDestroy());
        }
    }
    public int getShieldHealth()
    {
        return shieldHealth;
    }
}



