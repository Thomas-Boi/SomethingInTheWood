using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string DialogueName;

    public GameObject textBubble;

    private void Start()
    {
        DisplayTextBubble();
    }

    /// <summary>
    /// Display a text bubble near the player's head top right.
    /// </summary>
    private void DisplayTextBubble()
    {
        if (!textBubble) return;
        // need to do some math to shift the bubble around.
        Vector2 size = GetComponent<Renderer>().bounds.size;
        Vector2 position = (Vector2)transform.position + 2 * size;
        position.y -= size.y / 2;
        Instantiate(textBubble, position, Quaternion.identity);
    }

    public Dialogue Talk() 
    {
        return GameObject.Find("Canvas")
            .GetComponent<DialogueDisplayer>().DisplayDialogue(DialogueName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
