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

    /// <summary>
    /// Contain the current dialogue the player is seeing.
    /// Setting this will freeze the player's location until
    /// the dialogue is finished.
    /// </summary>
    public Dialogue CurDialogue {
        get { return curDialogue; }
        set 
        {
            movementScript.CanMove = false;
            curDialogue = value;
        }
    }

    // allows Player to set the CurDialogue when script starts
    private void Awake()
    {
        movementScript = GetComponent<ProtoMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Find all objects with Tag "Interactable" within the current scene
        interactables = GameObject.FindGameObjectsWithTag("Interactable");

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
            if (CurDialogue)
            {
                ContinueDialogue();
            }
            else if (interactObject?.GetComponent<Character>() != null)
            {
                StartDialogue(interactObject.GetComponent<Character>());
            }
        }

    }

    /// <summary>
    /// Start a new dialogue if there isn't one on the scene already.
    /// </summary>
    /// <param name="character">
    /// The character we are talking to.
    /// </param>
    private void StartDialogue(Character character)
    {
        CurDialogue = character.Talk();
    }

    /// <summary>
    /// Continue the dialogue if there's one.
    /// This will also allow the player to move again.
    /// </summary>
    private void ContinueDialogue()
    {
        if (!curDialogue.NextDialogue())
        {
            CurDialogue = null;
            movementScript.CanMove = true;
        }
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
