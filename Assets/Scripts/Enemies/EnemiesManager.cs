using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Variables
    public SlimeController slimeEnemy;
    private static bool enemiesManagerExists;

    // Gettes
    public SlimeController GetSlime() { return this.slimeEnemy; }

    // Start is called before the first frame update
    void Start()
    {
        if(!EnemiesManager.enemiesManagerExists)
        {
            EnemiesManager.enemiesManagerExists = true;
            // This code will make sure that the player will not be destroyed when change scenes
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(this.slimeEnemy == null) { this.slimeEnemy = FindObjectOfType<SlimeController>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
