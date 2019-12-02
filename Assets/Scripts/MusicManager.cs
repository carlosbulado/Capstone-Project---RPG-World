using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Variables
    protected static bool musicManagerExists;
    public AudioSource[] musicTracks;
    public int currentTrack;
    public bool musicCanPlay;


    // Start is called before the first frame update
    void Start()
    {
        // Fix the duplicates of MusicManager in the world
        if(!MusicManager.musicManagerExists)
        {
            MusicManager.musicManagerExists = true;
            // This code will make sure that this manager will not be destroyed when change scenes
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource music = this.musicTracks[this.currentTrack];
        if(this.musicCanPlay)
        {
            if(!music.isPlaying)
            {
                for (int i = 0; i < this.musicTracks.Length; i++)
                {
                    AudioSource _music = this.musicTracks[i];
                    if(i != this.currentTrack)
                    {
                        _music.Stop();
                    }
                }
                music.Play();
            }
        }
        else
        {
            foreach (AudioSource _music in this.musicTracks)
            {
                _music.Stop();
            }
        }
    }

    public void SwitchTrack(int newTrack)
    {
        this.currentTrack = newTrack;
    }
}
