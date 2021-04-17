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

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;

    private NavMeshAgent agent;
    private Animator anim;
    private SpriteRenderer rend;
    public bool aggro;
    public float knockbackTime;
    public int health;

    public float invincibleTime;

    // Start is called before the first frame update
    protected void Start()
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
            rend.color = new Color(1, 0, 0, 1);
        }
        else
        {
            rend.color = new Color(1, 1, 1, 1);
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
                playBoarAggroSound(transform.position);
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
        if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<Movement>().invincibleTime <= 0)
        {
            Vector2 direction = (transform.position - col.gameObject.transform.position).normalized;
            agent.velocity = new Vector2(0, 0);
            agent.speed = 0;
            knockbackTime = .5f;
            SoundManager.PlayOneClipAtLocation(AudioClips.singleton.playerHurt, col.gameObject.GetComponent<Player>().transform.position, 1.0f);
            col.gameObject.GetComponent<Movement>().knockbackTime = .2f;
            col.gameObject.GetComponent<Movement>().invincibleTime = 1f;
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -1000, ForceMode2D.Impulse);

            col.gameObject.GetComponent<Player>().currentHealth--;
            col.gameObject.GetComponent<Player>().UpdatePlayerHealth();

            Debug.Log(direction * 100);
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Bullet" && invincibleTime <= 0)
        {
            SoundManager.PlayOneClipAtLocation(AudioClips.singleton.fleshImpact, col.gameObject.transform.position, 0.3f);
            playBoarDamagedSound(col.gameObject.transform.position);
            aggro = true;
            Destroy(col.gameObject);
            agent.velocity = new Vector2(0, 0);
            agent.speed = 0;
            knockbackTime = 0.1f;
            invincibleTime = 0.1f;
            health--;
            if (health <= 0) {
                playBoarKilledSound(col.gameObject.transform.position);
                Destroy(gameObject);
                var args = new EnemyKilledEventArgs
                { 
                    enemyName = this.GetType().Name
                };
                EventTracker.GetTracker().EnemyWasKilled(this, args);
            }
        }
    }

    // Plays random sound from list of sounds for boar damage
    private void playBoarDamagedSound(Vector3 location)
    {
        List<AudioClip> clips = new List<AudioClip>();
        clips.Add(AudioClips.singleton.boarDamaged1);
        clips.Add(AudioClips.singleton.boarDamaged2);
        clips.Add(AudioClips.singleton.boarDamaged3);
        SoundManager.playRandomFromList(clips, location, 1);
    }
    private void playBoarKilledSound(Vector3 location)
    {
        List<AudioClip> clips = new List<AudioClip>();
        clips.Add(AudioClips.singleton.boarGrunt1);
        clips.Add(AudioClips.singleton.boarGrunt2);
        SoundManager.playRandomFromList(clips, location, 1);
    }
    private void playBoarAggroSound(Vector3 location)
    {
        List<AudioClip> clips = new List<AudioClip>();
        clips.Add(AudioClips.singleton.boarGrunt1);
        clips.Add(AudioClips.singleton.boarGrunt2);
        SoundManager.playRandomFromList(clips, location, 1);
    }


}
