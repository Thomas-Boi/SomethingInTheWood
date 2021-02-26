using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour
{
    // track the UI
    public GameObject dialoguePrefab;

    public Dialogue DisplayDialogue(string dialogueName)
    {
        var dialogueElem = Instantiate(dialoguePrefab, transform).GetComponent<Dialogue>();
        DialogueData data = Resources.Load<DialogueData>($"DialogueData/{dialogueName}");
        DialogueStruct[] dialogues = Resources.Load<DialogueData>($"DialogueData/{dialogueName}").dialogues;

        // show the first dialogue
        // after that, the onclick event handler will do the rest
        dialogueElem.StartDialogue(dialogues);
        dialogueElem.NextDialogue();
        return dialogueElem;
    }

}
