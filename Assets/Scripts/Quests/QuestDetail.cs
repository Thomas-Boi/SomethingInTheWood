using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestDetail : ScriptableObject
{
    public string description;
    public string itemName;
    public int amount;
    public QuestType questType;
}

/// <summary>
/// The quest types in the game.
/// </summary>
public enum QuestType
{
    TALK, // has to talk to a character
    ITEM, // has to get an item
    KILL // has to kill certain number of enemies
}