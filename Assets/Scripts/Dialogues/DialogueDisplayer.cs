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

    public Dialogue DisplayDialogue(string dialogueName)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();
        TextAsset data = Resources.Load<TextAsset>($"DialogueData/{dialogueName}");
        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(data.ToString());

        // show the first dialogue and pass the quest name to be created when
        // dialogue ends.
        DialogueEndedEventArgs args = new DialogueEndedEventArgs();
        args.questName = dialogueData.questName;
        dialogueElem.StartDialogue(dialogueData.dialogues, args);
        dialogueElem.NextDialogue();

        if (dialogueData.questName != "")
        {
            dialogueElem.DialogueEnded += questManager.AddQuest;
        }
        return dialogueElem;
    }

}
