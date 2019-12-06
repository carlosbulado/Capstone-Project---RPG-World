using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewArea : MonoBehaviour
{
    // Variables
    public string levelToLoad;
    public string exitPoint;
    protected PlayerController thePlayer;


    // Start is called before the first frame update
    void Start()
    {
        this.thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") { this.GoTo(); }
    }

    public void GoTo()
    {
        Application.LoadLevel(this.levelToLoad);
        if(this.thePlayer != null) { this.thePlayer.startPoint = this.exitPoint; }
    }
}
