using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes2D : MonoBehaviour
{ 
    public string Scene;
    public void LoadScene()
    {
        SceneManager.LoadScene(Scene);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
