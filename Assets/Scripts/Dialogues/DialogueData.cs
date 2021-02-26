using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// holds the data about a dialogue for a specific week/scenario
[CreateAssetMenu(fileName = "New Dialogue Data", menuName = "Dialogue")]
public class DialogueData : ScriptableObject 
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