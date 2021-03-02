using System;

/// <summary>
/// Holds multiple DialoguesStruct
/// </summary>
[Serializable]
public class DialogueData
{
    /// <summary>
    /// Holds a set of dialogues.
    /// </summary>
    public DialogueStruct[] dialogues;

    /// <summary>
    /// The name of a QuestDetail ScriptableObject that'll
    /// be called when this dialogue ends.
    /// </summary>
    public string questName;
}

// holds the data on a dialogue speaker and what they says
[Serializable]
public struct DialogueStruct
{
    public string speaker;
    public string speech;
}