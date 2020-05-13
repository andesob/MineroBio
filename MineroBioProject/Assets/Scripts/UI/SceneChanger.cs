using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    [SerializeField] private string toScene;
    private SceneController changeLevel;

    // Start is called before the first frame update
    void Start()
    {
        changeLevel = GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneController>();
    }

    public void ChangeScene(string scene)
    {
        changeLevel.LoadScene(scene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            changeLevel.LoadScene(toScene);
        }
    }
}
