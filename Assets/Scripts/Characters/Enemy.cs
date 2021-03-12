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
        agent.SetDestination(target.position);

        PlayAnimation();
        
    }

    // todo: Move this to a subclass
    void PlayAnimation() {
        anim.speed = Mathf.Abs(agent.velocity.x)/10;
        
        if (agent.velocity.x < 0) {
            rend.flipX = true;
        } else {
            rend.flipX = false;
        }

    }
}
