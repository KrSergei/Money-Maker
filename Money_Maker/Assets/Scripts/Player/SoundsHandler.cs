using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsHandler : MonoBehaviour
{

    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }

    public void StopSound(AudioSource audio)
    {
        audio.Stop();
    }
}
