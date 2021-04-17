using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load a new scene. Note: all load methods
/// will clear the EventTracker before it
/// loads a new scene.
/// </summary>
public class SceneLoader : MonoBehaviour
{

    // add new scene names here...
    static string START_MENU_NAME = "StartMenu";
    static string BEACH_SCENE_NAME = "Beach";
    static string JUNGLE_SCENE_NAME = "Jungle";

    // ALL load method must clear the EventTracker before
    // loading so the old objects are garbage collected
    public static void LoadStartMenu()
    {
        EventTracker.GetTracker().ClearEventHandlers();
        SceneManager.LoadScene(START_MENU_NAME);
    }

    public static void LoadBeachScene()
    {
        EventTracker.GetTracker().ClearEventHandlers();
        SceneManager.LoadScene(BEACH_SCENE_NAME);
    }

    public static void LoadJungleScene()
    {
        EventTracker.GetTracker().ClearEventHandlers();
        SceneManager.LoadScene(JUNGLE_SCENE_NAME);
    }
}
