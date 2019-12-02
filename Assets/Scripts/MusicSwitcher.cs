using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    // Variables
    protected MusicManager musicManager;
    public int newTrack;
    public bool switchOnStart;


    // Start is called before the first frame update
    void Start()
    {
        this.musicManager = FindObjectOfType<MusicManager>();

        if(this.switchOnStart)
        {
            this.musicManager.SwitchTrack(this.newTrack);
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            this.musicManager.SwitchTrack(this.newTrack);
            gameObject.SetActive(false);
        }
    }
}
