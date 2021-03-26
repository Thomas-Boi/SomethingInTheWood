using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Items that can be picked up by the player when
/// interact with.
/// </summary>
public class PickupItem : Item
{
    public static string DEFAULT_TAG = "QuestItem";
    public override string Interact()
    {
        Destroy(gameObject);
        return itemName;
    }

    public void BecomeInteractible()
    {
        tag = "Interactable";
    }
}
