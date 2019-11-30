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
        if(other.gameObject.name == "Player")
        {
            Application.LoadLevel(this.levelToLoad);
            this.thePlayer.startPoint = this.exitPoint;
        }
    }
}
