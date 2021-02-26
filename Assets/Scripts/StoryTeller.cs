using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This tells the story of the prototype.
/// </summary>
public class StoryTeller : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player.DialogueName = "Intro";
        player.Talk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
