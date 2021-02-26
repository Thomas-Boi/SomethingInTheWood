using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds multiple DialoguesStruct
/// </summary>
[Serializable]
public class DialogueData
{
    public DialogueStruct[] dialogues;
}

// holds the data on a dialogue speaker and what they says
[Serializable]
public struct DialogueStruct
{
    public string speaker;
    public string speech;
}