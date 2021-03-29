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

    public DialogueUI DisplayDialogue(string dialogueName)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<DialogueUI>();
        TextAsset data = Resources.Load<TextAsset>($"Dialogues/{dialogueName}");
        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(data.ToString());

        // show the first dialogue and pass the quest name to be created when
        // dialogue ends.
        dialogueElem.StartDialogue(dialogueData.dialogues);
        dialogueElem.NextDialogue();

        // some dialogues don't have a quest to trigger after
        if (dialogueData.questName != "")
        {
            RegisterQuestOnDiaglogueEnd(dialogueElem, dialogueData);
        }
        return dialogueElem;
    }

    /// <summary>
    /// Register the add quest code when dialogue ends.
    /// </summary>
    /// <param name="dialogueElem"></param>
    /// <param name="dialogueData"></param>
    private void RegisterQuestOnDiaglogueEnd(DialogueUI dialogueElem, DialogueData dialogueData) 
    {
        DialogueEndedEventArgs args = new DialogueEndedEventArgs();
        args.questName = dialogueData.questName;
        dialogueElem.args = args;
        dialogueElem.DialogueEnded += questManager.AddQuest;
    }

}
