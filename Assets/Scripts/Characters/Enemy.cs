using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    private Animator anim;
    private SpriteRenderer rend;
    public bool aggro;
    public float knockbackTime;
    public int health;
    
    public SpriteRenderer sprite;

    public float invincibleTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;

            sprite.color = new Color(1, 0, 0, 1);


        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1);
        }

        agent.SetDestination(target.position);

        PlayAnimation();

        if (knockbackTime > 0)
        {
            knockbackTime -= Time.deltaTime;
            agent.velocity = new Vector2(0, 0);
        }
        else
        {
        }


        // todo: Make aggro distances and speeds into variables
        if (!aggro)
        {
            if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 30)
            {
                aggro = true;
            }
            agent.speed = 0;


        }
        else if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) > 30 && knockbackTime <= 0)
        {
            agent.speed = 10;
        }
        else if (knockbackTime <= 0)
        {
            agent.speed = 30;
        }
        else
        {
            agent.speed = 0f;
        }
    }

    // todo: Move this to a subclass
    void PlayAnimation()
    {
        anim.speed = Mathf.Max(Mathf.Abs(agent.velocity.x), Mathf.Abs(agent.velocity.y)) / 10;

        if (agent.velocity.x < 0)
        {
            rend.flipX = true;
        }
        if (agent.velocity.x > 0)
        {
            rend.flipX = false;
        }

    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<ProtoMovement>().invincibleTime <= 0)
        {
            Vector2 direction = (transform.position - col.gameObject.transform.position).normalized;
            agent.velocity = new Vector2(0, 0);
            agent.speed = 0;
            knockbackTime = .5f;

            col.gameObject.GetComponent<ProtoMovement>().knockbackTime = .2f;
            col.gameObject.GetComponent<ProtoMovement>().invincibleTime = 1f;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -1000, ForceMode2D.Impulse);

            col.gameObject.GetComponent<Player>().currentHealth--;
            col.gameObject.GetComponent<Player>().UpdatePlayerHealth();

            Debug.Log(direction * 100);
        }
        

    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Bullet" && invincibleTime <= 0)
        {
            aggro = true;
            Object.Destroy(col.gameObject);
            agent.velocity = new Vector2(0, 0);
            agent.speed = 0;
            knockbackTime = 0.1f;
            invincibleTime = 0.1f;
            health--;
            if (health <= 0) {
                Object.Destroy(gameObject);
            }
        }
    }



}
