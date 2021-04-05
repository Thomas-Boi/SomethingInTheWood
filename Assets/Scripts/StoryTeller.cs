using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This tells the story of the prototype.
/// </summary>
public class StoryTeller : MonoBehaviour
{
    public Player player;
    public NPC John;
    private QuestManager questManager;

    // Start is called before the first frame update
    // set up all the dialogues and quests
    void Start()
    { // in the future, hopefully we can load this using a scriptable object.
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
        questManager.AddQuest("MeetJohnFirstTime");

        player.SetDialogue("Intro");
        player.TalkOutloud();

        John.DisplayTextBubble();
        John.SetDialogue("MeetJohnFirstTime");
    }

}
