using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    private QuestManager questManager;

    private void Start()
    {
        questManager = gameObject.GetComponent<QuestManager>();
    }

    public DialogueUI DisplayMainDialogue(DialogueData dialogueData)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<DialogueUI>();

        // show the first dialogue and pass the quest name to be created when
        // dialogue ends.
        dialogueElem.StartDialogue(dialogueData.mainDialogue);
        dialogueElem.NextDialogue();

        // some dialogues don't have a quest to trigger after
        if (dialogueData.questName != "")
        {
            RegisterQuestOnDiaglogueEnd(dialogueElem, dialogueData.questName);
        }
        return dialogueElem;
    }

    public DialogueUI DisplayIdleDialogue(DialogueData dialogueData)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<DialogueUI>();

        // show the first dialogue and pass the quest name to be created when
        // dialogue ends.
        dialogueElem.StartDialogue(dialogueData.idleDialogue);
        dialogueElem.NextDialogue();
        return dialogueElem;
    }

    /// <summary>
    /// Register the add quest code when dialogue ends.
    /// </summary>
    /// <param name="dialogueElem"></param>
    /// <param name="questName"></param>
    private void RegisterQuestOnDiaglogueEnd(DialogueUI dialogueElem, string questName) 
    {
        DialogueEndedEventArgs args = new DialogueEndedEventArgs();
        args.questName = questName;
        dialogueElem.args = args;
        dialogueElem.DialogueEnded += questManager.AddQuest;
    }

}
