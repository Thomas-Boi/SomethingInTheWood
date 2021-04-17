using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    public static AudioClips singleton;
    public AudioClip playerShot;
    public AudioClip shotImpact;
    public AudioClip fleshImpact;
    public AudioClip boarDamaged1;
    public AudioClip boarDamaged2;
    public AudioClip boarDamaged3;
    public AudioClip boarGrunt1;
    public AudioClip boarGrunt2;
    public AudioClip gunReload;
    public AudioClip gunEmpty;
    public AudioClip UITick;
    public AudioClip dialogTick;
    public AudioClip playerHurt;
    public AudioClip itemPickup;

    // Scene sounds
    public AudioClip craftCamp;
    public AudioClip campFire;





    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}