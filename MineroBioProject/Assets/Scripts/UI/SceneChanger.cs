using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{

    [SerializeField] private string toScene;
    private SceneController changeLevel;

    // Start is called before the first frame update
    void Start()
    {
        changeLevel = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            changeLevel.LoadScene(toScene);
        }
    }
}
