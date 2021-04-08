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
    private bool canMove;
    public float knockbackTime;
    public float invincibleTime;
    public SpriteRenderer sprite;



    /// <summary>
    /// Whether the player can move or not.
    /// If true, they can. If false, they can't.
    /// </summary>
    public bool CanMove
    {
        get { return canMove; }
        set
        {
            if (!value)
            {
                rb.velocity = new Vector2(0, 0);
            }
            canMove = value;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(movement.x, movement.y);
    }

    private void Start()
    {
        CanMove = true;
    }


    void Update()
    {
        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
        }

        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            if (Mathf.Round(invincibleTime * 20) % 3 == 0)
            {
                sprite.color = new Color(1, 1, 1, 0);
            }
            else
            {
                sprite.color = new Color(1, 1, 1, 1);
            }

        } else {
            sprite.color = new Color(1, 1, 1, 1);
        }


        // Player Inputs
        // WASD Movement, getting the direction from defined axes in input manager of Unity Project
        if (CanMove && knockbackTime <= 0)
        {
            float xDir = Input.GetAxisRaw("Horizontal") * movementSpeed;
            float yDir = Input.GetAxisRaw("Vertical") * movementSpeed;
            movement = new Vector2(xDir, yDir);
            Move();
        }


        // Aiming and Shooting
    }
}
