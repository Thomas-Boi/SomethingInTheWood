using System;

/// <summary>
/// Holds multiple DialoguesStruct
/// </summary>
[Serializable]
public class DialogueData
{
    /// <summary>
    /// Name of the dialogue json file. This should not
    /// be set in the JSON but rather set manually when
    /// it's loaded from JSON to object.
    /// </summary>
    public string dialogueName;

    /// <summary>
    /// Holds a set of dialogues that's relevant to the
    /// main story.
    /// </summary>
    public DialogueStruct[] mainDialogue;

    /// <summary>
    /// The name of a QuestDetail ScriptableObject that'll
    /// be called when this dialogue ends.
    /// </summary>
    public string nextQuest;

    /// <summary>
    /// The extra dialogues for when the character is idling around.
    /// </summary>
    public DialogueStruct[] idleDialogue;

    /// <summary>
    /// Tracks the name of the next dialogue when this one is done.
    /// </summary>
    public string nextDialogue;

    /// <summary>
    /// Tracks the name of the quest that needs to be completed 
    /// before we move on to the next dialogue.
    /// </summary>
    public string requiredQuestForNextDialogue;
}

// holds the data on a dialogue speaker and what they says
[Serializable]
public struct DialogueStruct
{
    public string speaker;
    public string speech;
}