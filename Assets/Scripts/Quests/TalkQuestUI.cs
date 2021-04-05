﻿public class TalkQuestUI : QuestUI
{
    /// <summary>
    /// Update a quest's progress.
    /// </summary>
    /// <returns>
    /// true if quest is completed. Else false.
    /// </returns>
    public override bool UpdateProgress()
    {
        // if name matches in CheckQuestItem()
        // then that means we already spoke to the person we need
        Destroy(gameObject);
        // triggers the end event handler
        var args = new QuestEventArgs()
        {
            questName = detail.questName
        };
        EventTracker.GetTracker().QuestHasEnded(this, args);
        return true;
    }

}
