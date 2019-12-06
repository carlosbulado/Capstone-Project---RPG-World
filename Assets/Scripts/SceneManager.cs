using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Variables
    public EnemiesManager enemiesManager;
    private static bool sceneManagerExists;
    public PlayerStartPoint[] respawnPoints;
    protected bool[] respawnPointsEnemyAlive;
    protected float[] respawnCounter;
    protected EnemyController[] allEnemiesOnScene;
    protected int nextRespawn;

    // Start is called before the first frame update
    void Start()
    {
        if(!SceneManager.sceneManagerExists)
        {
            SceneManager.sceneManagerExists = true;
            // This code will make sure that the player will not be destroyed when change scenes
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if(this.enemiesManager == null) { this.enemiesManager = FindObjectOfType<EnemiesManager>(); }

        if(this.respawnPoints != null)
        {
            this.respawnPointsEnemyAlive = new bool[this.respawnPoints.Length];
            this.respawnCounter = new float[this.respawnPoints.Length];
            this.allEnemiesOnScene = new EnemyController[this.respawnPoints.Length];
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.allEnemiesOnScene = FindObjectsOfType<EnemyController>();

        for (int i = 0; i < this.respawnPointsEnemyAlive.Length ; i++) { this.respawnPointsEnemyAlive[i] = false; }

        for (int i = 0; i < this.allEnemiesOnScene.Length ; i++)
        {
            var item = this.allEnemiesOnScene[i];
            this.respawnPointsEnemyAlive[item.numEnemyOnScene] = true;
        }

        if(this.respawnPoints != null && this.respawnPoints.Length > 0)
        {
            this.nextRespawn = Dice.Roll(respawnPoints.Length);
            nextRespawn--;
            if(!this.respawnPointsEnemyAlive[nextRespawn] && this.respawnCounter[nextRespawn] <= 0)
            {
                // DB: Loads all sprites for this scene
                EnemyController newEmeny = this.enemiesManager.GetSlime();
                var enemyClone = (EnemyController)Instantiate(newEmeny, this.respawnPoints[nextRespawn].transform.position, this.respawnPoints[nextRespawn].transform.rotation);
                this.respawnPointsEnemyAlive[nextRespawn] = true;
                this.respawnCounter[nextRespawn] = newEmeny.respawnTime;
                enemyClone.gameObject.SetActive(true);
                enemyClone.numEnemyOnScene = nextRespawn;
            }
        }

        for (int i = 0; i < this.respawnCounter.Length; i++)
        {
            if(!this.respawnPointsEnemyAlive[i])
            {
                this.respawnCounter[i] -= Time.deltaTime;
            }
        }
    }
}
