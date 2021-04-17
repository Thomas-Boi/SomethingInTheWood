using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sound manager controls playing sounds for all of the recurring sounds in game. (With the exception of 1 or 2 special cases)
/// </summary>
public static class SoundManager
{

    private static bool currentlyPlayingAmbient = false;

    // Plays clip at location using AudioSource.playClipAtPoint, destroys audio source after playing clip
    // I know it seems redundant but having easy access to the references through this class is valuable
    // ** Use when audio needs to be played in one shot at one location without moving
    public static void PlayOneClipAtLocation(AudioClip sound, Vector3 location, float volume) // Volume is very important in adjusting for diagetic sound, default diagetic sound works well with this method if volume is tuned instead of chosen arbitrarily on a clip by clip basis
    {
        AudioSource.PlayClipAtPoint(sound, location, volume); // Creates an audio source at the location then plays one shot through it with the specified clip
    }
    public static void PlayOneClip(AudioClip sound, float volume) // Volume is very important in adjusting for diagetic sound, default diagetic sound works well with this method if volume is tuned instead of chosen arbitrarily on a clip by clip basis
    {
        float x = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
        float y = GameObject.FindGameObjectWithTag("MainCamera").transform.position.y;
        AudioSource.PlayClipAtPoint(sound, new Vector3(x, y, 0), volume); // Creates an audio source at the location then plays one shot through it with the specified clip
    }


    // Plays a looping sound from an audio source in the player's camera.
    public static void StartAmbientSound(AudioClip sound, float volume)
    {
        AudioSource ambientAudioSource;

        // Grab a reference to the mainCamera, we want to play the audio from here since the main audio listener is here and the sound is ambient in the entire scene
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera.GetComponent<AudioSource>() == null) // If there isn't already an audio source on the main cam, add one
        {
            ambientAudioSource = mainCamera.AddComponent<AudioSource>();
            ambientAudioSource.loop = true; // Want to loop scene's sound for entirety of scene
        }
        else
        {
            ambientAudioSource = mainCamera.GetComponent<AudioSource>();
        }

        ambientAudioSource.clip = sound;
        ambientAudioSource.volume = volume;
        ambientAudioSource.Play();
        currentlyPlayingAmbient = true;
    }

    // Quick functions to pause and resume existing ambient sound
    public static void PauseAmbientSound()
    {
        AudioSource ambientAudioSource;
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera.GetComponent<AudioSource>() == null) // If there isn't already an audio source on the main cam then there is nothing to pause
        {
            return;
        }
        else // Pause the existing audio source
        {
            ambientAudioSource = mainCamera.GetComponent<AudioSource>();
            ambientAudioSource.Pause();
            currentlyPlayingAmbient = false;
        }
    }
    public static void ResumeAmbientSound()
    {
        AudioSource ambientAudioSource;
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera.GetComponent<AudioSource>() == null) // If there isn't already an audio source on the main cam then there is nothing to resume
        {
            return;
        }
        else // Resume the audio that was playing
        {
            ambientAudioSource = mainCamera.GetComponent<AudioSource>();
            ambientAudioSource.Play();
            currentlyPlayingAmbient = true;
        }
    }


    // Just for player walk, it has dedicated methods due to diff nature of looping sound based on control inputs
    public static void PlayPlayerWalk()
    {
        AudioSource ambientAudioSource;
        GameObject walk = GameObject.FindGameObjectWithTag("walkSoundSrc");

        if (walk.GetComponent<AudioSource>() == null) // If there isn't already an audio source on the main cam then there is nothing to resume
        {
            return;
        }
        else // Resume the audio that was playing
        {
            ambientAudioSource = walk.GetComponent<AudioSource>();
            ambientAudioSource.Play();
        }
    }

    public static void PausePlayerWalk()
    {
        AudioSource ambientAudioSource;
        GameObject walk = GameObject.FindGameObjectWithTag("walkSoundSrc");

        if (walk.GetComponent<AudioSource>() == null) // If there isn't already an audio source on the main cam then there is nothing to resume
        {
            return;
        }
        else // Resume the audio that was playing
        {
            ambientAudioSource = walk.GetComponent<AudioSource>();
            ambientAudioSource.Pause();
        }
    }

    public static void startLoopingSoundOnObjectWithTag(string tag)
    {
        // grab audio source from tagged object
        AudioSource src = GameObject.FindGameObjectWithTag(tag).GetComponent<AudioSource>();
        src.Play();
    }

    public static void stopLoopingSoundOnObjectWithTag(string tag)
    {
        // grab audio source from tagged object
        AudioSource src = GameObject.FindGameObjectWithTag(tag).GetComponent<AudioSource>();
        src.Pause();
    }


    public static void playRandomFromList(List<AudioClip> clips, Vector3 location, float volume) // Volume is very important in adjusting for diagetic sound, default diagetic sound works well with this method if volume is tuned instead of chosen arbitrarily on a clip by clip basis
    {
        int index = Random.Range(0, clips.Count);
        AudioClip sound = clips[index];
        AudioSource.PlayClipAtPoint(sound, location, volume); // Creates an audio source at the location then plays one shot through it with the specified clip
        Debug.Log("PLAYING CLIP #" + index + " CLIP NAME: " + sound.name);
    }
}








