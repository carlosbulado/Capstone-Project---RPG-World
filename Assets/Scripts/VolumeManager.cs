using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    // Variables
    protected VolumeController[] allAudios;
    public float currentVolumeLevel;
    protected float maxVolumeLevel = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        this.allAudios = FindObjectsOfType<VolumeController>();

        if(this.currentVolumeLevel > this.maxVolumeLevel)
        {
            this.currentVolumeLevel = this.maxVolumeLevel;
        }

        foreach (VolumeController audio in this.allAudios)
        {
            audio.SetAudioLevel(this.currentVolumeLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (VolumeController audio in this.allAudios)
        {
            audio.SetAudioLevel(this.currentVolumeLevel);
        }
    }
}
