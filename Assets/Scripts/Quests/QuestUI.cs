using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// The args for a quest start/end event.
/// Contains the questName of the quest.
/// </summary>
public class QuestEventArgs : EventArgs
{
    /// <summary>
    /// The name of a QuestDetail ScriptableObject.
    /// </summary>
    public string questName;
}
public abstract class QuestUI : MonoBehaviour
{
    // refs to UI elements
    public Text descriptionTxt;

    public QuestDetail detail;

    public void StartQuest(QuestDetail _detail)
    {
        detail = _detail;
        descriptionTxt.text = detail.description;
    }

    /// <summary>
    /// Update a quest's progress.
    /// </summary>
    /// <returns>
    /// true if quest is completed. Else false.
    /// </returns>
    public abstract bool UpdateProgress();

    /// <summary>
    /// Check whether the object is what this quest is looking for.
    /// </summary>
    /// <param name="name">
    /// The object (character/monster/item) name needs to be checked.
    /// </param>
    /// <returns>whether it is what we are looking for.</returns>
    public bool CheckObject(string name)
    {
        return name == detail.itemName;
    }
}
