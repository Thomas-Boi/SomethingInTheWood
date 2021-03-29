using System;

/// <summary>
/// Holds multiple DialoguesStruct
/// </summary>
[Serializable]
public class DialogueData
{
    /// <summary>
    /// Holds a set of dialogues that's relevant to the
    /// main story.
    /// </summary>
    public DialogueStruct[] mainDialogue;

    /// <summary>
    /// The name of a QuestDetail ScriptableObject that'll
    /// be called when this dialogue ends.
    /// </summary>
    public string questName;

    /// <summary>
    /// The extra dialogues for when the character is idling around.
    /// </summary>
    public DialogueStruct[] idleDialogue;
}

// holds the data on a dialogue speaker and what they says
[Serializable]
public struct DialogueStruct
{
    public string speaker;
    public string speech;
}