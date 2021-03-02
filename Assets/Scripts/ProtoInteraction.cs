using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProtoInteraction
{

    [SerializeField]
    private float interactRange = 55.0f;
    private GameObject[] interactables;

    // the player can only interact with one object at a time
    private GameObject interactObject;

    private QuestManager questManager;

    private readonly Player player;

    public ProtoInteraction(Player player)
    {
        this.player = player;
        // find all things that can be interact with
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    /// <param name="isTalking">
    /// Whether the player is talking (either to themselves
    /// or others).
    /// </param>
    public void Update(bool isTalking)
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
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
            if (isTalking)
            {
                player.HandleDialogue(null); // player already know they are talking
                return;
            }

            if (interactObject?.GetComponent<Character>()) // is a character
            {
                player.HandleDialogue(interactObject.GetComponent<Character>());
                return;
            }

            if (interactObject?.GetComponent<Item>()) // is an item
            {
                string itemName = interactObject.GetComponent<Item>().Interact();
                if (questManager.CheckQuestItem(itemName))
                {
                    // recheck the interactables in case one got destroyed
                    interactables = GameObject.FindGameObjectsWithTag("Interactable");
                }
            }

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
        interactObject = null;
        return false;
    }

    
}
