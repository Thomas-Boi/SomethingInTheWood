using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    public DialogueUI DisplayMainDialogue(DialogueData dialogueData)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<DialogueUI>();

        // show the first dialogue and pass the quest name to be created when
        // dialogue ends.
        dialogueElem.StartDialogue(dialogueData.mainDialogue, dialogueData);
        dialogueElem.NextDialogue();
        return dialogueElem;
    }

    public DialogueUI DisplayIdleDialogue(DialogueData dialogueData)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<DialogueUI>();

        // show the first dialogue and pass the quest name to be created when
        // dialogue ends.
        dialogueElem.StartDialogue(dialogueData.idleDialogue, dialogueData);
        dialogueElem.NextDialogue();
        return dialogueElem;
    }

}
