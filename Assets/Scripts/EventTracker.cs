using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks the main events that happen in the game.
/// Specifically quests, and dialogues. This is a
/// singleton object and acts as a wrapper class
/// around QuestUI and DialogueUI.
/// </summary>
public class EventTracker
{
    private static EventTracker instance;
    private EventTracker()
    {

    }

    public static EventTracker GetTracker()
    {
        if (instance == null)
        {
            instance = new EventTracker();
        }
        return instance;
    }

    public event EventHandler<DialogueEndedEventArgs> DialogueEnded;
    public event EventHandler<QuestEndedEventArgs> QuestEnded;

    public void QuestHasEnded(object src, QuestEndedEventArgs args)
    {
        QuestEnded?.Invoke(src, args);
    }

    public void DialogueHasEnded(object src, DialogueEndedEventArgs args)
    {
        DialogueEnded?.Invoke(src, args);
    }
}
