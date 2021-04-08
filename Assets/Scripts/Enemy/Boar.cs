using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    public const int MAX_HEALTH = 10;

    new void Start()
    {
        base.Start();
        health = MAX_HEALTH;
    }
}
