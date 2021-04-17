﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{

    public float farAwaySpeed;




    protected override void farAwayBehavior()
    {
        base.farAwayBehavior();
        agent.speed = farAwaySpeed;
    }


    protected override void PlayAnimation()
    {

        anim.speed = Mathf.Sqrt(Mathf.Pow(agent.velocity.x, 2) + Mathf.Pow(agent.velocity.y, 2)) / 10;

        if (agent.velocity.x < 0)
        {
            rend.flipX = true;
        }
        if (agent.velocity.x > 0)
        {
            rend.flipX = false;
        }
    }
}
