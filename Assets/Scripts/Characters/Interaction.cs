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
    private Color objectColor;
    private bool promptEnabled;

    private QuestManager questManager;

    // dialogues
    private DialogueUI curDialogue;
    private Character characterTalkingWith;

    // weapon
    public Image weaponImage;


    public Interaction(Player player)
    {
        this.player = player;
        // find all things that can be interact with
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
        promptEnabled = false;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");

        // If object is within designated range of player, allow player to interact 
        // For testing purposes, change the color of TestNPC to let the player know they are in range
        // Later we should have a state that lets the player know they are in range
        // E.g. Change sprite to show colored outline, and when out of range, revert to sprite without outline
        if (ObjectInRange())
        {
            if (!promptEnabled)
            {
                objectColor = interactObject.GetComponent<SpriteRenderer>().color;
                promptEnabled = true;
            }
            interactObject.GetComponent<SpriteRenderer>().color = Color.red;
            
        }
        else
        {
            if (interactObject != null)
            {
                interactObject.GetComponent<SpriteRenderer>().color = objectColor;
                promptEnabled = false;
                interactObject = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // sometimes player talks to themselves
            if (curDialogue != null)
            {
                SoundManager.PlayOneClip(AudioClips.singleton.dialogTick, 1.0f);
                ContinueTalking();
                return;
            }

            if (!interactObject) return;

            if (interactObject.GetComponent<NPC>()) // is an NPC
            {
                SoundManager.PlayOneClip(AudioClips.singleton.dialogTick, 1.0f);
                StartTalking(interactObject.GetComponent<NPC>());
            }

            if (interactObject.GetComponent<Item>()) // is an item
            {
                SoundManager.PlayOneClip(AudioClips.singleton.itemPickup, 1.0f);
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
