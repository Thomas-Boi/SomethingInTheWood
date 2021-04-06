using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    public DialogueUI DisplayMainDialogue(DialogueData dialogueData)
    {
        bool displayMain = true;
        return DisplayDialogue(dialogueData, displayMain);
    }

    public DialogueUI DisplayIdleDialogue(DialogueData dialogueData)
    {
        bool displayMain = false;
        return DisplayDialogue(dialogueData, displayMain);
    }

    private DialogueUI DisplayDialogue(DialogueData dialogueData, bool displayMain)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<DialogueUI>();

        // show the first dialogue
        dialogueElem.StartDialogue(dialogueData, displayMain);
        dialogueElem.NextDialogue();
        return dialogueElem;
    }

}
