using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FeedMyKids1.Utilities
{
public class SceneManagement : MonoBehaviour
{
    

    public void Quit()
    {
        if (Debug.isDebugBuild)
            Debug.Log("Quitting!");
        Application.Quit();
    }
}
}
