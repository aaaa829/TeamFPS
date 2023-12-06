using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // [SerializeField] private string nextScene;

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     LoadingScene();
        // }
    }

    public void LoadingScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
