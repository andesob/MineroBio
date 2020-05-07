using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (anim)
        {
            Debug.Log("Animator object found:" + anim.name);
        }
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
    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") && collider.isTrigger && shieldHealth == 2)

        {
            shieldHealth -= 1;
            print("YOU ARE HERE");
            anim.Play("ff_dmg");
        }
        else if (collider.CompareTag("enemy") && collider.isTrigger && shieldHealth == 1)
        {
            anim.Play("ff_destroy");
            StartCoroutine(WaitToDestroy());
        }
    }*/
       
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
            print("YOU ARE HERE");
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



