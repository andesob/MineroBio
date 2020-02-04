using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("MainCharacter");
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

    void FadeOut()
    {

    }
}
