using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // dialogues
    protected DialogueData curDialogue;

    protected DialogueManager dialogueManager;

    public GameObject textBubblePrefab;
    protected GameObject textBubble;

    public string charName;

    public void Awake()
    {
        dialogueManager = GameObject.Find("Canvas").GetComponent<DialogueManager>();
    }

    /// <summary>
    /// Display a text bubble near the player's head top right.
    /// </summary>
    public void DisplayTextBubble()
    {
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
    }


    /// <summary>
    /// Talk to the player. 
    /// </summary>
    /// <returns>
    /// A dialogue object that can be used to continue the dialogue.
    /// </returns>
    public virtual DialogueUI Talk() 
    {
        return dialogueManager.DisplayMainDialogue(curDialogue);
    }
}
