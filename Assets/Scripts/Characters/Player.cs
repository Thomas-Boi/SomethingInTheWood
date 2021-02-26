using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private ProtoInteraction interaction;

    void Awake()
    {
        interaction = GetComponent<ProtoInteraction>();
    }

    public new Dialogue Talk()
    {
        interaction.CurDialogue = base.Talk();
        return interaction.CurDialogue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
