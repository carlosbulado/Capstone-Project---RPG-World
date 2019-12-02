using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Variables
    protected static bool audioManagerExists;

    // Start is called before the first frame update
    void Start()
    {
        // Fix the duplicates of AudioManager in the world
        if(!AudioManager.audioManagerExists)
        {
            AudioManager.audioManagerExists = true;
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
        
    }
}
