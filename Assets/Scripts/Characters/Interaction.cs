using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


/// <summary>
/// Handle the keyboard inputs and player 
/// interaction with the world.
/// </summary>
public class Interaction
{
    private readonly Player player;

    // interaction
    // the player can only interact with one object at a time
    private float interactRange = 55.0f;
    private GameObject[] interactables;
    private GameObject interactObject;
    private GameObject interactPrompt;

    private Color objectColor;
    private bool promptEnabled;

    private QuestManager questManager;

    // dialogues
    private DialogueUI curDialogue;
    private Character characterTalkingWith;

    // weapon
    public Image weaponImage;

    // canvas
    private Canvas canvas;

    public Interaction(Player player)
    {
        this.player = player;
        // find all things that can be interact with
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        interactPrompt = player.interactPrompt;
        promptEnabled = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");

        // If object is within designated range of player, allow player to interact 
        if (ObjectInRange())
        {
            if (promptEnabled)
            {
                interactPrompt.SetActive(false);
            } else
            {
                Vector2 objectPos = interactObject.transform.position;
                Vector2 displayPos = objectPos;
                displayPos.y -= 2;
                interactPrompt.SetActive(true);
                interactPrompt.transform.position = displayPos;
            }
        }
        else
        {
            promptEnabled = false;
            if (interactObject != null)
            {
                interactPrompt.SetActive(false);
                interactObject = null;
            }
            if (!interactObject)
            {
                interactPrompt.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // sometimes player talks to themselves
            if (curDialogue != null)
            {
                ContinueTalking();
                return;
            }

            if (!interactObject) return;

            if (interactObject.GetComponent<NPC>()) // is an NPC
            {
                StartTalking(interactObject.GetComponent<NPC>());
                promptEnabled = true;
            }

            if (interactObject.GetComponent<Item>()) // is an item
            {
                string itemName = interactObject.GetComponent<Item>().Interact();
                questManager.CheckQuestItem(itemName);
            }

        }

        // if kill, get object name, pass to quest manager
    }

    /// <summary>
    /// Continue the dialogue if there's one. Else, 
    /// create a new Dialogue by talking to the character.
    /// </summary>
    /// <param name="character">
    /// The character we are talking to.
    /// </param>
    /// <returns>
    /// True if the dialogue is finished. Else false.
    /// </returns>
    public void StartTalking(Character character)
    {
        player.FreezeMovement(true);
        curDialogue = character.Talk();
        characterTalkingWith = character;
    }

    /// <summary>
    /// Continue talking with the current dialogue.
    /// </summary>
    public void ContinueTalking()
    {
        bool hasNextDialogue = curDialogue.NextDialogue();
        if (!hasNextDialogue)
        {
            questManager.CheckQuestItem(characterTalkingWith.charName);
            curDialogue = null;
            characterTalkingWith = null;
            player.FreezeMovement(false);
        }
    }

    /// <summary>
    /// Checks if an interactable object is within designated range of player
    /// </summary>
    /// <returns></returns>
    private bool ObjectInRange()
    {
        foreach (GameObject go in interactables)
        {
            float distance = (go.transform.position - player.transform.position).sqrMagnitude;
            if (distance <= interactRange)
            {
                interactObject = go;
                return true;
            }
        }
        return false;
    }

    
}
