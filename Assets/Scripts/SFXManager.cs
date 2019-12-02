using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Variables
    protected static bool sfxManagerExists;
    public AudioSource playerHurt;
    public AudioSource playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        // Fix the duplicates of SFXManager in the world
        if(!SFXManager.sfxManagerExists)
        {
            SFXManager.sfxManagerExists = true;
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
