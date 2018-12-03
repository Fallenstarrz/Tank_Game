using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    // audio source to receive sounds
    public AudioSource soundSource;
    // audio clips to play from audio source
    public AudioClip hoverSound;
    public AudioClip pressedSound;

    // set audio source to audio source from parent
    private void Start()
    {
        soundSource = GetComponentInParent<AudioSource>();
    }

    // play sound on hover, call from UI using event trigger
    public void playHover()
    {
        soundSource.clip = hoverSound;
        soundSource.Play();

    }

    // play sound on click, call from UI using on clicked event trigger
    public void playPressed()
    {
        soundSource.clip = pressedSound;
        if (soundSource.isPlaying == true)
        {
            return;
        }
        else
        {
            soundSource.Play();
        }
    }
}
