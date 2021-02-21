using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoMovement : MonoBehaviour
{
    // Using serialize field so we can adjust speed in editor
    [SerializeField]
    private float movementSpeed = 5.0f;

    void Update()
    {
        // Kinda crappy input management for now... Will need to be replaced or refined as dev progresses and proto phase complete.
        // Movement is not physics based, rigid bodies for the player and enemy are going to be used for collisions only but NOT physics.
        // Bullets may use physics as it's more useful in that context if we want projectile bullets.

        // Player Inputs
        // WASD Movement, getting the direction from defined axes in input manager of Unity Project
        float xDir = Input.GetAxisRaw("Horizontal");
        transform.Translate(movementSpeed * xDir * Time.deltaTime, 0, 0);

        float yDir = Input.GetAxisRaw("Vertical");
        transform.Translate(0, movementSpeed * yDir * Time.deltaTime, 0);

        // Aiming and Shooting
    }
}
