using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProtoInteraction : MonoBehaviour
{

    [SerializeField]
    private float interactRange = 55.0f;
    private GameObject[] interactables;

    private GameObject interactObject;

    private Dialogue curDialogue;

    private ProtoMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with Tag "Interactable" within the current scene
        interactables = GameObject.FindGameObjectsWithTag("Interactable");

        movementScript = GetComponent<ProtoMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        // If object is within designated range of player, allow player to interact 
        // For testing purposes, change the color of TestNPC to let the player know they are in range
        // Later we should have a state that lets the player know they are in range
        // E.g. Change sprite to show colored outline, and when out of range, revert to sprite without outline

        if (ObjectInRange())
        {
            var sprOutline = interactObject.GetComponent<SpriteRenderer>().color;
            sprOutline = Color.red;
            interactObject.GetComponent<SpriteRenderer>().color = sprOutline;
        }
        else
        {
            if (interactObject != null)
            {
                var sprOutline = interactObject.GetComponent<SpriteRenderer>().color;
                sprOutline = Color.yellow;
                interactObject.GetComponent<SpriteRenderer>().color = sprOutline;
                interactObject = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (interactObject != null && interactObject.GetComponent<NPC>() != null)
            {
                HandleDialogue(interactObject.GetComponent<NPC>().DialogueName);
            } 
        }

    }

    /// <summary>
    /// Deal with the dialogues.
    /// Start a new dialogue if there isn't one on the scene already.
    /// If there's one, go to the next line.
    /// </summary>
    /// <param name="dialogueName">
    /// The name of the dialogue we are displaying.
    /// </param>
    private void HandleDialogue(string dialogueName)
    {
        if (curDialogue)
        {
            if (!curDialogue.NextDialogue())
            {
                curDialogue = null;
                movementScript.CanMove = true;
            }
            return;
        }
        curDialogue = GameObject.Find("Canvas")
            .GetComponent<DialogueDisplayer>().DisplayDialogue(dialogueName);
        movementScript.CanMove = false;
    }

    private bool ObjectInRange()
    {
        foreach (GameObject go in interactables)
        {
            float distance = (go.transform.position - transform.position).sqrMagnitude;
            if (distance <= interactRange)
            {
                interactObject = go;
                return true;
            }
        }
        return false;
    }

    
}
