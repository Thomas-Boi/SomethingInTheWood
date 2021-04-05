using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Character : MonoBehaviour
{
    // dialogues
    private DialogueData curDialogue;
    // there are two types: the main dialogue
    // vs the idle dialogue after the main one
    // is spoken
    private bool spokenMainDialogue;
    private DialogueManager dialogueDisplayer;
    private Dictionary<string, string> questDialogueDict;

    public GameObject textBubblePrefab;
    private GameObject textBubble;
    public string charName;

    public void Awake()
    {
        dialogueDisplayer = GameObject.Find("Canvas").GetComponent<DialogueManager>();
        spokenMainDialogue = false;

        // register the event handler so we know when a quest ended
        var questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
        questManager.OnQuestEnded += OnQuestEndHandler;
    }

    /// <summary>
    /// Display a text bubble near the player's head top right.
    /// </summary>
    public void DisplayTextBubble()
    {
        if (!textBubble) return;
        // need to do some math to shift the bubble around.
        Vector2 size = GetComponent<Renderer>().bounds.size;
        Vector2 position = (Vector2)transform.position + 2 * size;
        position.y -= size.y / 2;
        textBubble = Instantiate(textBubblePrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// Set the dialogue that this character will talk about.
    /// </summary>
    public void SetDialogue(string dialogueResourceName)
    {
        TextAsset data = Resources.Load<TextAsset>($"Dialogues/{dialogueResourceName}");
        curDialogue = JsonUtility.FromJson<DialogueData>(data.ToString());
        spokenMainDialogue = false;
    }

    public void SetQuestDialogueDict(string jsonResourceName)
    {
        TextAsset data = Resources.Load<TextAsset>($"QuestDialogueDict/{jsonResourceName}");
        questDialogueDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(data.ToString());
    }

    /// <summary>
    /// Handle the event when a quest ended. This checks
    /// whether the Character needs to do anything (animation, change dialogue etc...)
    /// </summary>
    /// <param name="source">The source QuestManager</param>
    /// <param name="args">The argument containing the quest name that ended</param>
    public void OnQuestEndHandler(object source, QuestEndedEventArgs args)
    {
        string dialogueName;
        questDialogueDict.TryGetValue(args.questName, out dialogueName);
        if (dialogueName != null)
        {
            SetDialogue(dialogueName);
        }
    }

    /// <summary>
    /// Talk to the player. 
    /// </summary>
    /// <returns>
    /// A dialogue object that can be used to continue the dialogue.
    /// </returns>
    public DialogueUI Talk() 
    {
        if (textBubble)
        {
            Destroy(textBubble);
        }

        if (!spokenMainDialogue)
        {
            spokenMainDialogue = true;
            return dialogueDisplayer.DisplayIdleDialogue(curDialogue);
        }

        return dialogueDisplayer.DisplayMainDialogue(curDialogue);
    }
}
