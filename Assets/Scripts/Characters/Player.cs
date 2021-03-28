using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private ProtoInteraction interactionScript;

    private ProtoMovement movementScript;

    private DialogueUI curDialogue;

    private QuestManager questManager;

    // Player stats
    public HealthBar playerHealthBar;
    public int maxHealth;
    public int currentHealth;

    void Awake()
    {
        interactionScript = new ProtoInteraction(this);
        movementScript = GetComponent<ProtoMovement>();
        questManager = GameObject.Find("Canvas").GetComponent<QuestManager>();
    }

    void Start()
    {
        playerHealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        bool isTalking = curDialogue != null;
        interactionScript.Update(isTalking);
        UpdatePlayerHealth();
    }

    /// <summary>
    /// Updates the player's current health when they recover or take damage.
    /// </summary>
    public void UpdatePlayerHealth()
    {
        playerHealthBar.SetHealth(this.currentHealth);
    }

    /// <summary>
    /// Continue the dialogue if there's one. Else, 
    /// create a new Dialogue by talking to the character.
    /// </summary>
    /// <param name="character">
    /// The character we are talking to.
    /// </param>
    public void HandleDialogue(Character character)
    {
        if (curDialogue)
        { // continue dialogue
            bool hasNextDialogue = curDialogue.NextDialogue();
            if (!hasNextDialogue)
            {// dialogue finished
                curDialogue = null;
                movementScript.CanMove = true;
            }
        }
        else
        { // start a dialogue
            movementScript.CanMove = false;
            curDialogue = character.Talk();
        }
    }

    /// <summary>
    /// Make the player talk outloud to themselves.
    /// </summary>
    public void TalkOutloud()
    {
        HandleDialogue(this);
    }

    
}
