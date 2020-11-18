using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioSource[] sounds;

    private void Start()
    {
        sounds = GetComponents<AudioSource>();
    }

    public void Jump()
    {
        sounds[0].Play();
        //print("playing sound");
    }

    public void Footsteps()
    {
        sounds[1].Play();
    }

    public void ChaserAlert()
    {
        sounds[2].Play();
    }

    public void PlayerDeath()
    {
        sounds[3].Play();
    }

       

    public void GetGem()
    {
        sounds[4].Play();
    }


}
