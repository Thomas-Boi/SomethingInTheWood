using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The args for a enemy killed event.
/// Contains the enemy name.
/// </summary>
public class EnemyKilledEventArgs : EventArgs
{
    /// <summary>
    /// The name of the killed enemy.
    /// </summary>
    public string enemyName;
}

public abstract class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Animator anim;
    protected SpriteRenderer rend;

    [SerializeField]
    protected Transform target;
    protected Color normalColor;


    public float hitstunSeconds = 0.1f;
    public float hitInvincibilitySeconds = 0.1f;
    public float speed;
    public float acceleration;
    public int health;

    public float aggroDistance;
    public float farAwayDistance;

    public bool aggro;

    //hidden stuff
    //[HideInInspector]
    public float invincibleTime = 0;
    //[HideInInspector]
    public float knockbackTime = 0;
    

    // Start is called before the first frame update
    protected void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.acceleration = acceleration;

        normalColor = rend.color;


    }

    // Update is called once per frame
    protected void Update()
    {
        HandleInvincible();


        agent.SetDestination(target.position);

        PlayAnimation();

        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
            agent.velocity = new Vector2(0, 0);
        }


        if (!aggro)
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < aggroDistance)
            {
                aggro = true;
            }
            agent.speed = 0;
        }
        else if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > farAwayDistance && knockbackTime <= 0)
        {
            farAwayBehavior();
        }
        else if (knockbackTime <= 0)
        {
            agent.speed = speed;
        }
        else
        {
            agent.speed = 0;
        }
    }

    // todo: Move this to a subclass
    protected abstract void PlayAnimation();



    protected virtual void farAwayBehavior()
    {
        agent.speed = speed;
    }

    protected virtual void HandleInvincible()
    {
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            rend.color = new Color(1, 0, 0, 1);
        }
        else
        {
            rend.color = normalColor;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<Movement>().invincibleTime <= 0)
        {
            Vector2 direction = (transform.position - col.gameObject.transform.position).normalized;
            agent.velocity = new Vector2(0, 0);
            agent.speed = 0;
            knockbackTime = .5f;

            col.gameObject.GetComponent<Movement>().knockbackTime = .2f;
            col.gameObject.GetComponent<Movement>().invincibleTime = 1f;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -1000, ForceMode2D.Impulse);

            col.gameObject.GetComponent<Player>().currentHealth--;
            col.gameObject.GetComponent<Player>().UpdatePlayerHealth();

            Debug.Log(direction * 100);
        }


    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet" && invincibleTime <= 0)
        {
            aggro = true;
            Destroy(col.gameObject);
            agent.velocity = new Vector2(0, 0);
            agent.speed = 0;
            knockbackTime = hitstunSeconds;
            invincibleTime = hitInvincibilitySeconds;
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
                var args = new EnemyKilledEventArgs
                {
                    enemyName = this.GetType().Name
                };
                EventTracker.GetTracker().EnemyWasKilled(this, args);
            }
        }
    }



}
