using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    void playSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

}
