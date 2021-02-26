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
        TextAsset data = Resources.Load<TextAsset>($"DialogueData/{dialogueName}");
        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(data.ToString());

        // show the first dialogue
        // after that, the onclick event handler will do the rest
        dialogueElem.StartDialogue(dialogueData.dialogues);
        dialogueElem.NextDialogue();
        return dialogueElem;
    }

}
