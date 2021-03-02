using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This tells the story of the prototype.
/// </summary>
public class StoryTeller : MonoBehaviour
{
    public Player player;
    public Character John;

    // Start is called before the first frame update
    // set up all the dialogues and quests
    void Start()
    { // in the future, hopefully we can load this using a scriptable object.
        player.DialogueName = "Intro";
        player.TalkOutloud();

        John.DisplayTextBubble();
        John.DialogueName = "ProtoDialogue";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
