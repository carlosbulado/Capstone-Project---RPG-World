using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGWorldCapstone
{
    public class SceneManager : MonoBehaviour
    {
        // Variables
        public PlayerController player;
        public bool isPlayerRespawn;
        public float timeToPlayerRespawn;
        public EnemiesManager enemiesManager;
        private static bool sceneManagerExists;
        public PlayerStartPoint[] respawnPoints;
        protected bool[] respawnPointsEnemyAlive;
        protected float[] respawnCounter;
        protected EnemyController[] allEnemiesOnScene;
        protected int nextRespawn;

        public GameObject whoHit;
        public GameObject whoGotHit;

        public float recoil;

        public float timeToAutoRegen;
        protected float timeToAutoRegenCounter;

        // Setters
        public void SetObjectsThatTouch(GameObject hit, GameObject took)
        {
            this.whoHit = hit;
            this.whoGotHit = took;
            var obj1X = this.whoHit.transform.position.x;
            var obj1Y = this.whoHit.transform.position.y;
            var obj2X = this.whoGotHit.transform.position.x;
            var obj2Y = this.whoGotHit.transform.position.y;

            var newObj1X = obj1X <= obj2X ? obj1X - this.recoil : obj1X + this.recoil;
            var newObj1Y = obj1Y <= obj2Y ? obj1Y - this.recoil : obj1Y + this.recoil;

            var newObj2X = obj1X <= obj2X ? obj2X + this.recoil : obj2X - this.recoil;
            var newObj2Y = obj1Y <= obj2Y ? obj2Y + this.recoil : obj2Y - this.recoil;

            //this.whoHit.transform.position = new Vector2(newObj1X, newObj1Y);
            this.whoGotHit.transform.position = new Vector2(newObj2X, newObj2Y);
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!SceneManager.sceneManagerExists)
            {
                SceneManager.sceneManagerExists = true;
                // This code will make sure that the player will not be destroyed when change scenes
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            if (this.enemiesManager == null) { this.enemiesManager = FindObjectOfType<EnemiesManager>(); }

            if (this.respawnPoints != null)
            {
                this.respawnPointsEnemyAlive = new bool[this.respawnPoints.Length];
                this.respawnCounter = new float[this.respawnPoints.Length];
                this.allEnemiesOnScene = new EnemyController[this.respawnPoints.Length];
            }

            if(this.player == null) { this.player = FindObjectOfType<PlayerController>(); }
            if(this.timeToPlayerRespawn <= 0f) { this.timeToPlayerRespawn = 5f; }
            if(this.recoil <= 0f) { this.recoil = .5f; }
            if(this.timeToAutoRegen <= 0f) { this.timeToAutoRegen = 2.5f; }
            this.timeToAutoRegenCounter = this.timeToAutoRegen;
        }

        // Update is called once per frame
        void Update()
        {
            this.checkPlayer();
            this.checkRespawnMonsters();
            this.checkPlayerAutoRegen();
        }

        void checkPlayer()
        {
            if(this.isPlayerRespawn)
            {
                if (player.GetStats().GetCurrentHealth() <= 0)
                {
                    this.player.gameObject.SetActive(false);
                }

                if(!this.player.gameObject.active)
                {
                    this.timeToPlayerRespawn -= Time.deltaTime;
                }
                if(this.timeToPlayerRespawn <= 0)
                {
                    this.timeToPlayerRespawn = 5f;
                    this.player.GetStats().RecoverFullHealth();
                    this.player.gameObject.SetActive(true);
                    this.player.GetAnimator().SetBool("PlayerAttacking", false);
                }
            }
        }

        void checkRespawnMonsters()
        {
            this.allEnemiesOnScene = FindObjectsOfType<EnemyController>();

            for (int i = 0; i < this.respawnPointsEnemyAlive.Length; i++) { this.respawnPointsEnemyAlive[i] = false; }

            for (int i = 0; i < this.allEnemiesOnScene.Length; i++)
            {
                var item = this.allEnemiesOnScene[i];
                this.respawnPointsEnemyAlive[item.numEnemyOnScene] = true;
            }

            if (this.respawnPoints != null && this.respawnPoints.Length > 0)
            {
                this.nextRespawn = Dice.Roll(respawnPoints.Length);
                nextRespawn--;
                if (!this.respawnPointsEnemyAlive[nextRespawn] && this.respawnCounter[nextRespawn] <= 0)
                {
                    // DB: Loads all sprites for this scene
                    EnemyController newEmeny = this.enemiesManager.GetSlime();
                    var enemyClone = (EnemyController)Instantiate(newEmeny, this.respawnPoints[nextRespawn].transform.position, this.respawnPoints[nextRespawn].transform.rotation);
                    this.respawnPointsEnemyAlive[nextRespawn] = true;
                    this.respawnCounter[nextRespawn] = newEmeny.respawnTime;
                    enemyClone.gameObject.transform.position = new Vector3(enemyClone.gameObject.transform.position.x, enemyClone.gameObject.transform.position.y, 0);
                    enemyClone.gameObject.SetActive(true);
                    enemyClone.numEnemyOnScene = nextRespawn;
                }
            }

            for (int i = 0; i < this.respawnCounter.Length; i++)
            {
                if (!this.respawnPointsEnemyAlive[i])
                {
                    this.respawnCounter[i] -= Time.deltaTime;
                }
            }
        }

        void checkPlayerAutoRegen()
        {
            if(!this.player.IsMoving())
            {
                if(this.timeToAutoRegenCounter > 0)
                {
                    this.timeToAutoRegenCounter -= Time.deltaTime;
                }
                else
                {
                    this.player.GetStats().RecoverHealth((int)this.player.GetStats().GetStrength() / 2);
                    this.timeToAutoRegenCounter = this.timeToAutoRegen;
                }
            }
            else
            {
                this.timeToAutoRegenCounter = this.timeToAutoRegen;
            }
        }
    }
}
