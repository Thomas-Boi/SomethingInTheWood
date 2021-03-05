using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkQuestDetail : QuestDetail
{
    /// <summary>
    /// The name of a Character.
    /// </summary>
    public string charName;

    /// <summary>
    /// Checks whether the name matches the condition
    /// of this QuestDetail.
    /// </summary>
    /// <param name="name">
    /// The name of a Character.
    /// </param>
    public override bool CheckCondition(string name)
    {
        return name == itemName;
    }
}
