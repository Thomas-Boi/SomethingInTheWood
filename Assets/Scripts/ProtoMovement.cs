using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoMovement : MonoBehaviour
{
    // Using serialize field so we can adjust speed in editor
    [SerializeField]
    private float movementSpeed = 5.0f;
    public Rigidbody2D rb;
    private Vector3 movement;

    /// <summary>
    /// Whether the player can move or not.
    /// If true, they can. If false, they can't.
    /// </summary>
    public bool CanMove = true;

    private void Move() {
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    void Update()
    {
        // Kinda crappy input management for now... Will need to be replaced or refined as dev progresses and proto phase complete.
        // Movement is not physics based, rigid bodies for the player and enemy are going to be used for collisions only but NOT physics.
        // Bullets may use physics as it's more useful in that context if we want projectile bullets.

        // Player Inputs
        // WASD Movement, getting the direction from defined axes in input manager of Unity Project
        if (CanMove)
        {
            float xDir = Input.GetAxisRaw("Horizontal") * movementSpeed;
            float yDir = Input.GetAxisRaw("Vertical") * movementSpeed;
            movement = new Vector2(xDir, yDir);
            Move();
        }


        // Aiming and Shooting
    }
}
