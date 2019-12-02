using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    // Variables
    protected AudioSource audio;
    protected float audioLevel;
    public float defaultAudioLevel;

    // Setters
    public void SetAudioLevel(float value)
    {
        if(this.audioLevel == null) { this.audio = GetComponent<AudioSource>(); }

        this.audioLevel = this.defaultAudioLevel * value;
        this.audio.volume = this.audioLevel;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
