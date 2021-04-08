﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    // there are two types: the main dialogue
    // vs the idle dialogue after the main one
    // is spoken
    private bool spokenMainDialogue;

    // Start is called before the first frame update
    void Start()
    {
        // register the event handler so we know when a quest ended
        EventTracker.GetTracker().QuestEndedHandler += OnQuestEndHandler;
        EventTracker.GetTracker().DialogueEndedHandler += OnDialogueEndHandler;
        spokenMainDialogue = false;
    }

    /// <summary>
    /// Set the dialogue that this character will talk about.
    /// </summary>
    public new void SetDialogue(string dialogueResourceName)
    {
        base.SetDialogue(dialogueResourceName);
        spokenMainDialogue = false;
    }

    /// <summary>
    /// Handle the event when a quest ended.This checks whether
    /// we can proceed to the next dialogue when a quest has ended.
    /// </summary>
    /// <param name="source">The source QuestManager</param>
    /// <param name="args">The argument containing the quest name that ended</param>
    private void OnQuestEndHandler(object source, QuestEventArgs args)
    {
        if (args.questName == curDialogue.requiredQuestForNextDialogue)
        {
            SetDialogue(curDialogue.nextDialogue);
        }
    }

    /// <summary>
    /// Checks whether we need to set a dialogue ourselves next.
    /// </summary>
    /// <param name="source">The source QuestManager</param>
    /// <param name="args">The argument containing the quest name that ended</param>
    private void OnDialogueEndHandler(object source, DialogueEventArgs args)
    {
        if (args.dialogueData.dialogueName != curDialogue.dialogueName) return;

        if (!string.IsNullOrEmpty(curDialogue.nextDialogue) 
            && string.IsNullOrEmpty(curDialogue.requiredQuestForNextDialogue))
        {
            SetDialogue(curDialogue.nextDialogue);
        }
    }

    /// <summary>
    /// Talk to the player. 
    /// </summary>
    /// <returns>
    /// A dialogue object that can be used to continue the dialogue.
    /// </returns>
    public override DialogueUI Talk()
    {
        if (textBubble)
        {
            Destroy(textBubble);
        }

        // this is usually for quest dialogues that has idle dialogues
        if (spokenMainDialogue)
        {
            return dialogueManager.DisplayIdleDialogue(curDialogue);
        }

        spokenMainDialogue = true;
        return dialogueManager.DisplayMainDialogue(curDialogue);
    }
}