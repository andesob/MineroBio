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
        DamageTaken();
        ForceFieldDestroy();
    }
    void FollowPlayer()
    {
        float x = Player.transform.position.x;
        float y = Player.transform.position.y;

        transform.position = new Vector2(x, y);

    }

    void DamageTaken()
    {
       
            if (Input.GetKeyDown(KeyCode.K)  && shieldHealth == 2)
            {
            shieldHealth -= 1;
            print("YOU ARE HERE");
            anim.Play("ff_dmg");
                
            }
        }
    void ForceFieldDestroy()
    {
        if(Input.GetKeyDown(KeyCode.L) && shieldHealth == 1)
        {
            anim.Play("ff_destroy");
            StartCoroutine(WaitToDestroy());
           
            
        }
    }
   IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds (0.4f);
        Destroy(gameObject);
    }
    }



