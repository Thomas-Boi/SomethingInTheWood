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
    // singleton
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

    // event handlers
    public event EventHandler<DialogueEventArgs> DialogueEndedHandler;
    public event EventHandler<QuestEventArgs> QuestStartedHandler;
    public event EventHandler<QuestEventArgs> QuestEndedHandler;
    public event EventHandler<EnemyKilledEventArgs> EnemyKilledHandler;

    /// <summary>
    /// Signal that a quest has started.
    /// </summary>
    /// <param name="src"></param>
    /// <param name="args"></param>
    public void QuestHasStarted(object src, QuestEventArgs args)
    {
        QuestStartedHandler?.Invoke(src, args);
    }

    /// <summary>
    /// Signal that a quest has ended.
    /// </summary>
    /// <param name="src"></param>
    /// <param name="args"></param>
    public void QuestHasEnded(object src, QuestEventArgs args)
    {
        QuestEndedHandler?.Invoke(src, args);
    }

    /// <summary>
    /// Signal that a dialogue has ended.
    /// </summary>
    /// <param name="src"></param>
    /// <param name="args"></param>
    public void DialogueHasEnded(object src, DialogueEventArgs args)
    {
        DialogueEndedHandler?.Invoke(src, args);
    }

    /// <summary>
    /// Signal that an enemy has been killed.
    /// </summary>
    /// <param name="args"></param>
    public void EnemyWasKilled(object src, EnemyKilledEventArgs args)
    {
        EnemyKilledHandler?.Invoke(src, args);
    }

    /// <summary>
    /// Clear all the event handlers.
    /// </summary>
    public void ClearEventHandlers()
    {
        DialogueEndedHandler = null;
        QuestStartedHandler = null;
        QuestEndedHandler = null;
        EnemyKilledHandler = null;
}
}
