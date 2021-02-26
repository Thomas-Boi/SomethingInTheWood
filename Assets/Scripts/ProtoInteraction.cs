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

            // Interact with 'E' key
            if (Input.GetKeyDown(KeyCode.E))
            {
                // This action is currently for testing purposes
                // Feel free to change the interaction when 'E' is pressed to initialize dialogue
                Debug.Log("Hi there!");
            }
        } else
        {
            if (interactObject != null)
            {
                var sprOutline = interactObject.GetComponent<SpriteRenderer>().color;
                sprOutline = Color.yellow;
                interactObject.GetComponent<SpriteRenderer>().color = sprOutline;
            }
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
