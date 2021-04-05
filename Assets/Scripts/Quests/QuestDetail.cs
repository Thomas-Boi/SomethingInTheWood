using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestDetail : ScriptableObject
{
    /// <summary>
    /// Name of the ScriptableObject file this is in.
    /// Don't need to be set manually since this should be
    /// set when it's loaded from file system into memory.
    /// </summary>
    public string questName;

    /// <summary>
    /// Description of what the quest is. This will be displayed
    /// on the UI to guide the user.
    /// </summary>
    public string description;

    /// <summary>
    /// The item name that will satisfy this quest. Can also be
    /// a character or monster name. As long as it's interactable,
    /// it's good.
    /// </summary>
    public string itemName;

    /// <summary>
    /// Amount of itemName needed to finish this quest.
    /// </summary>
    public int amount;

    /// <summary>
    /// The quest type so we can create the appropriate UI.
    /// </summary>
    public QuestType questType;

    // allow chaining of quests if this quest is completed
    public string nextQuestName;
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