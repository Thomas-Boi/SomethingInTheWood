using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // helper scripts
    private Interaction interactionScript;
    private ProtoMovement movementScript;

    // Player stats
    public HealthBar playerHealthBar;
    public int maxHealth;
    public int currentHealth;

    new void Awake()
    {
        interactionScript = new Interaction(this);
        movementScript = GetComponent<ProtoMovement>();
        base.Awake();
    }

    void Start()
    {

        playerHealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        interactionScript.Update();
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
    /// Make the player talk outloud to themselves.
    /// </summary>
    public void TalkOutloud()
    {
        interactionScript.StartTalking(this);
    }

    /// <summary>
    /// Set whether the player movement is freezed.
    /// </summary>
    /// <param name="freeze"></param>
    public void FreezeMovement(bool freeze)
    {
        movementScript.CanMove = !freeze;
    }

    
}
