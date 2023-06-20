using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    

    public void Quit()
    {
        if (Debug.isDebugBuild)
            Debug.Log("Quitting!");
        Application.Quit();
    }
}
