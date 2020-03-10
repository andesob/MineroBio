using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAi : MonoBehaviour
{
    public enum State
    {
        roaming,
        chase,
        shoot,
        goingBackToStart

    }

    private EnemyMovement enemyMovement;
    public GameObject player;
    private Vector3 startPos;
    private State state;

    public float detectionDistance;
    public float maxDistance;

   

    private void Awake()
    {
        enemyMovement = this.gameObject.GetComponent<EnemyMovement>();
        state = State.chase;
        startPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /* private void FixedUpdate()
    {
        switch (state)
        {
            case State.roaming:
                if(Vector3.Distance(startPos,enemyMovement.GetPlayerPosition()) < detectionDistance)
                {
                    state = State.chase;
                }
                break;

            case State.chase:
                if(player.transform.position.x - this.gameObject.transform.position.x > maxDistance || transform.position.y > maxDistance)
                {

                }

        }
    }*/
}
