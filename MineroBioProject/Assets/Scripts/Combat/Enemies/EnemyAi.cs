using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public enum State
    {
        Roaming,
        chase,
        GoingBackToStart,
    }

    private EnemyMovement enemyMovement;
    private Vector3 startPosition;
    private State state;

    private float maximumDistance = 10f;
    private Vector2 roamPosition;

    void Awake() {
        enemyMovement = GetComponent<EnemyMovement>();
        state = State.chase;
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        roamPosition = enemyMovement.getRandomDir();
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void FixedUpdate()
    {
        switch(state)
        {
            case State.Roaming:

                /*float maxRoamingDir = 5f;
                enemyMovement.Roam(roamPosition);

                if (Vector3.Distance(transform.position, roamPosition) < maxRoamingDir)
                {
                    // Reached Roam Position
                    roamPosition = enemyMovement.getRandomDir();
                }
                */
                if(Vector3.Distance(startPosition, enemyMovement.getPlayerPosition()) < maximumDistance){
                    state = State.chase;
                }
                break;

            case State.chase:
                enemyMovement.MoveEnemyAfterPlayer();
                if(Vector3.Distance(transform.position, enemyMovement.startPosition) > maximumDistance)
                {
                    state = State.GoingBackToStart;
                }
                break;

            case State.GoingBackToStart:
                enemyMovement.MoveEnemyTowardSpawn();
                if (Vector3.Distance(startPosition, enemyMovement.getPlayerPosition()) < maximumDistance)
                {
                    state = State.chase;
                }
                if (Vector3.Distance(transform.position, startPosition) < 1f)
                {
                    state = State.Roaming;
                }
                break;
        }
       
    }
}
