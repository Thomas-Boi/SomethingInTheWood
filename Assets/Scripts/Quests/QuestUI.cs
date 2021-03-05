using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    // refs to UI elements
    public Text descriptionTxt;
    public Text progressTxt;

    // hold the dialogues
    private QuestDetail detail;
    private int curAmount;

    // start a new quest
    // need to call this first after instantiaing a Quest Prefab
    public void StartQuest(QuestDetail _detail)
    {
        detail = _detail;
        curAmount = 0;

        descriptionTxt.text = detail.description;
        progressTxt.text = $"{curAmount}/{detail.amount}";
    }

    /// <summary>
    /// Update a quest's progress.
    /// </summary>
    /// <returns>
    /// true if quest is completed. Else false.
    /// </returns>
    public bool UpdateProgress()
    {
        progressTxt.text = $"{++curAmount}/{detail.amount}";
        bool finished = curAmount == detail.amount;
        if (finished) Destroy(gameObject);
        return finished;
    }

    /// <summary>
    /// Check where the item is what this quest is looking for.
    /// </summary>
    /// <param name="item">
    /// The Item needs to be checked.
    /// </param>
    /// <returns>whether it is what we are looking for.</returns>
    public bool CheckItem(string itemName)
    {
        return itemName == detail.itemName;
    }

}
