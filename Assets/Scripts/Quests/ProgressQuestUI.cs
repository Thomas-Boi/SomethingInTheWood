using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tracks quest that requires a progress. 
/// Usually QuestType.KILL or QuestType.ITEM quest
/// </summary>
public class ProgressQuestUI : QuestUI
{
    // refs to UI elements
    public Text progressTxt;

    protected int curAmount;

    // start a new quest
    // need to call this first after instantiaing a Quest Prefab
    public new void StartQuest(QuestDetail _detail)
    {
        base.StartQuest(_detail);
        curAmount = 0;
        progressTxt.text = $"{curAmount}/{detail.amount}";
    }

    /// <summary>
    /// Update a quest's progress.
    /// </summary>
    /// <returns>
    /// true if quest is completed. Else false.
    /// </returns>
    public override bool UpdateProgress()
    {
        progressTxt.text = $"{++curAmount}/{detail.amount}";
        bool finished = curAmount == detail.amount;
        if (finished)
        {
            Destroy(gameObject);
            // triggers the end event handler
            var args = new QuestEventArgs()
            {
                questName = detail.questName
            };
            EventTracker.GetTracker().QuestHasEnded(this, args);
        }
        return finished;
    }

}
