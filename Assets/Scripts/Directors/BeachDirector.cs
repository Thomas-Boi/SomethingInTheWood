using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This directs how things happen in the Beach Scene.
/// </summary>
public class BeachDirector : MonoBehaviour
{
    // UI
    public Texture2D mouseCursor;

    // scene objects
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

        EventTracker.GetTracker().QuestStartedHandler += SpawnBoars;
        EventTracker.GetTracker().DialogueEndedHandler += MakeCamp;
        EventTracker.GetTracker().DialogueEndedHandler += LoadJungle;

        Cursor.SetCursor(mouseCursor, Vector2.zero, CursorMode.Auto);
    }

    void SpawnBoars(object src, QuestEventArgs args)
    {
        if (args.questName == "HuntBoars")
        {
            foreach (Transform child in GameObject.Find("Boars").transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    void MakeCamp(object src, DialogueEventArgs args)
    {
        if (args.dialogueData.dialogueName == "FinishedGatherWood")
        {
            // Play sounds for camp fire
            StartCoroutine(playCampAssemblySounds());
        }
    }

    void LoadJungle(object src, DialogueEventArgs args)
    {
        if (args.dialogueData.dialogueName == "CampFireTalk")
        {
            SceneLoader.LoadJungleScene();
        }
    }

    IEnumerator playCampAssemblySounds()
    {
        SoundManager.PlayOneClip(AudioClips.singleton.craftCamp, 1f);

        yield return new WaitForSeconds(1.9f);

        foreach (Transform child in GameObject.Find("Camp").transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}