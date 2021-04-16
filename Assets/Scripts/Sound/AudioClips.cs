using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    public static AudioClips singleton;
    public AudioClip playerShot;
    public AudioClip shotImpact;
    public AudioClip gunReload;
    public AudioClip gunEmpty;
    public AudioClip UITick;
    public AudioClip dialogTick;
    public AudioClip playerHurt;
    public AudioClip itemPickup;





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