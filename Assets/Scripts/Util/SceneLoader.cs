using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    // add new scene names here...
    static string BEACH_SCENE_NAME = "Beach";

    public static void LoadBeachScene()
    {
        SceneManager.LoadScene(BEACH_SCENE_NAME);
    }
}
