using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // dialogues
    private DialogueData curDialogue;
    // there are two types: the main dialogue
    // vs the idle dialogue after the main one
    // is spoken
    private bool spokenMainDialogue;
    private DialogueDisplayer dialogueDisplayer;

    public GameObject textBubblePrefab;
    private GameObject textBubble;
    public string charName;

    public void Awake()
    {
        dialogueDisplayer = GameObject.Find("Canvas").GetComponent<DialogueDisplayer>();
        spokenMainDialogue = false;
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
        Instantiate(textBubblePrefab, position, Quaternion.identity);
    }

    /// <summary>
    /// Set the dialogue that this character will talk about.
    /// </summary>
    public void SetDialogue(string dialogueResourceName)
    {
        TextAsset data = Resources.Load<TextAsset>($"Dialogues/{dialogueResourceName}");
        curDialogue = JsonUtility.FromJson<DialogueData>(data.ToString());
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

        if (spokenMainDialogue)
        {
            return dialogueDisplayer.DisplayIdleDialogue(curDialogue);
        }
        spokenMainDialogue = true;

        return dialogueDisplayer.DisplayMainDialogue(curDialogue);
    }
}
