using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Audio Sources
    public AudioSource FinishGameSource;
    public AudioSource DeadPlayerSource;
    //Audio Clips
    public AudioClip FinishGameClip;
    public AudioClip DeadPlayerClip;

    public void FinishSound()
    {
        FinishGameSource.PlayOneShot(FinishGameClip);
    }
    public void DeadPlayerSound()
    {
        DeadPlayerSource.PlayOneShot(DeadPlayerClip);
    }
}
