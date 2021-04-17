using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The args for a dialogue end event.
/// Contains the dialogueData of the dialogue.
/// </summary>
public class DialogueEventArgs : EventArgs
{
    /// <summary>
    /// The name of a QuestDetail ScriptableObject.
    /// </summary>
    public DialogueData dialogueData;

    /// <summary>
    /// Whether the dialogue was the main dialogue or idle dialogue.
    /// </summary>
    public bool displayedMain;
}

// holds the Text elements to display a dialogue piece from characters
public class DialogueUI : MonoBehaviour
{
    // refs to UI elements
    public Text speakerTxt;
    public Text speechTxt;
    public Image profilePic;

    // hold the dialogues
    private DialogueStruct[] dialogues;
    private int curScriptIndex;

    // holds the DialogueData where this dialogue came from
    public DialogueEventArgs args;

    /// <summary>
    /// start a new series of dialogues.
    /// need to call this first after instantiaing a Dialogue Prefab
    /// the combatHUDTransform is the UI canvas element's transform
    /// </summary>
    /// <param name="data">
    /// The dialogue data that we are extracting the dialogues from.
    /// </param>
    /// <param name="displayMain">
    /// Whether we are display the main dialogue or the idle dialogue.
    /// </param>
    public void StartDialogue(DialogueData data, bool displayMain)
    {
        dialogues = displayMain ? data.mainDialogue : data.idleDialogue;
        curScriptIndex = 0;
        args = new DialogueEventArgs
        {
            dialogueData = data,
            displayedMain = displayMain
        };
    }


    // display the texts in the dialogueObj on the screen
    // if there's none, delete the dialogue
    // returns true if there are 
    /// <summary>
    /// Get the next dialogue. If there's no more, destroy
    /// the dialogue UI object.
    /// </summary>
    /// <returns>
    /// true if there is a next dialogue. Else false.
    /// </returns>
    public bool NextDialogue()
    {
        try 
        {
            DialogueStruct dialogueObj = dialogues[curScriptIndex++];
            speechTxt.text = dialogueObj.speech;
            speakerTxt.text = dialogueObj.speaker;
            if (dialogueObj.speaker == "Help")
            {
                profilePic.sprite = Resources.Load<Sprite>($"CharacterPic/question_marks");
            }
            else
            {
                profilePic.sprite = Resources.Load<Sprite>($"CharacterPic/{dialogueObj.speaker.ToLower()}");
            }

        } 
        catch (IndexOutOfRangeException)
        {
            Destroy(gameObject);
            EventTracker.GetTracker().DialogueHasEnded(this, args);
            return false;
        }
        return true;
    }
}