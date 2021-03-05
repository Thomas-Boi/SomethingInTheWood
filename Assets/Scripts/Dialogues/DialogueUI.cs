using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// holds the Text elements to display a dialogue piece from characters
public class DialogueUI : MonoBehaviour
{
    // refs to UI elements
    public Text speakerTxt;
    public Text speechTxt;
    //public Image profilePic;

    // hold the dialogues
    private DialogueStruct[] dialogues;
    private int curScriptIndex;

    // holds a quest name that will be added to
    // the quest manager when the dialogue ends.
    private DialogueEndedEventArgs args;

    public event EventHandler<DialogueEndedEventArgs> DialogueEnded;

    /// <summary>
    /// start a new series of dialogues.
    /// need to call this first after instantiaing a Dialogue Prefab
    /// the combatHUDTransform is the UI canvas element's transform
    /// </summary>
    /// <param name="dialogues">
    /// The dialogues that will be displayed.
    /// </param>
    /// <param name="args">
    /// The args that will be passed to the event handler. 
    /// </param>
    public void StartDialogue(DialogueStruct[] dialogues, DialogueEndedEventArgs args)
    {
        this.dialogues = dialogues;
        this.args = args;
        curScriptIndex = 0;
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
            //if (dialogueObj.speaker == "Help")
            //{
            //    profilePic.sprite = Resources.Load<Sprite>($"Profiles/question_marks");
            //}
            //else
            //{
            //    profilePic.sprite = Resources.Load<Sprite>($"Profiles/{dialogueObj.speaker.ToLower()}");
            //}

        } 
        catch (IndexOutOfRangeException)
        {
            Destroy(gameObject);
            DialogueEnded?.Invoke(this, args);
            return false;
        }
        return true;
    }
}

public class DialogueEndedEventArgs : EventArgs
{
    /// <summary>
    /// The name of a QuestDetail ScriptableObject.
    /// </summary>
    public string questName;
}