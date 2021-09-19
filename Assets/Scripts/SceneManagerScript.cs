using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public int currentScene;

    // Update is called once per frame
    void Update()
    {
        if(currentScene == 0)
        {
            if (Input.GetKey("space"))
            {
                ToPlay();
            }
        }
        else if (currentScene == 1)
        {
            if (Input.GetKey("space"))
            {
                ToQuit();
            }
        }
    }

    public void ToPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void ToQuit()
    {
        SceneManager.LoadScene(0);
    }
}
