using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Find Item Quest", menuName = "FindItemQuest")]
public class FindItemQuestDetail : QuestDetail
{
    /// <summary>
    /// The name of a PickableItem Prefab.
    /// </summary>
    public string itemName;

    /// <summary>
    /// The amount the player needs to find.
    /// </summary>
    public int amount;

    /// <summary>
    /// Checks whether the name matches the condition
    /// of this QuestDetail.
    /// </summary>
    /// <param name="name">
    /// The name of a PickableItem.
    /// </param>
    /// <returns>
    /// Whether the name matches the condition.
    /// </returns>
    public override bool CheckCondition(string name)
    {
        return name == itemName;
    }
}
