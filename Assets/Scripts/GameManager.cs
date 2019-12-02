using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables
    private static bool isGameManagerExists;
    public PlayerController player;
    public EnemyController[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        this.player = FindObjectOfType<PlayerController>();
        this.enemies = FindObjectsOfType<EnemyController>();
        // Make sure there is just one GameManager
        if(!GameManager.isGameManagerExists)
        {
            GameManager.isGameManagerExists = true;
            // This code will make sure that the player will not be destroyed when change scenes
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
