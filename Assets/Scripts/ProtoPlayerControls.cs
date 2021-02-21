using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoPlayerControls : MonoBehaviour
{
    // Reference to the gun object on the TestPlayer prefab. Hookup on start.
    private GameObject gun;
    private GameObject gunChamber;

    [SerializeField]
    private float movementSpeed = 5.0f;

    void Start()
    {
        // Hookup gun references. Doing it by tag for now, if that's bad we can change the method.
        gun = GameObject.FindGameObjectWithTag("PlayerGun");
        // This one is really just a gameobject containing a transform position to instantiate bullet objects from
        gunChamber = GameObject.FindGameObjectWithTag("PlayerGunChamber"); 
    }

    void Update()
    {
        // Kinda crappy input management for now... Will need to be replaced or refined as dev progresses and proto phase complete.
        // Movement is not physics based, rigid bodies are going to be used for collisions NOT physics.

        // Player Inputs
        // WASD Movement, getting the direction from defined axes in input manager of Unity Project
        float xDir = Input.GetAxisRaw("Horizontal");
        transform.Translate(movementSpeed * xDir * Time.deltaTime, 0, 0);

        float yDir = Input.GetAxisRaw("Vertical");
        transform.Translate(0, movementSpeed * yDir * Time.deltaTime, 0);

        // Aiming and Shooting
    }
}
