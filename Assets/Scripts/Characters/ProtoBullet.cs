using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoBullet : MonoBehaviour
{
    // The rigid body for bullets will be used for collisions AND physics.
    Rigidbody2D rb;

    [SerializeField]
    private float movementMultiplier = 50.0f;
    [SerializeField]
    private float flightTime = 2.0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); // Hookup rigidbody
        rb.velocity = transform.right * movementMultiplier; // Set the velocity so it is ready once bullet starts
        Destroy(gameObject, flightTime); // Destroy it after flying for the specified time.
    }

    // On creation the bullet should just move in the direction it was spawned at
    void Update()
    {
        // DID NOT USE THIS METHOD AS INERTIA AFFECTS BULLET INITIAL VELOCITY HERE
        // THE METHOD IN START WORKS BETTER FOR BULLETS SPECIFICALLY

        // We can use this for spears though! Inertia would be good to build up to peak velocity when throwing spears.
        // For bullets that feels way to unnatural though
        //rb.AddForce(transform.right * movementMultiplier);
    }
}
