using System.Collections;
using System.Collections.Generic;
//using Photon.Pun;
//using Photon.Realtime;
using UnityEngine;

namespace RPGWorldCapstone
{
    public class GameManager : MonoBehaviour
    {
        // Variables
        private static bool isGameManagerExists;
        [Header("RPG World Capstone - player")]
        public PlayerController player;
        public static PlayerController globalPlayer;

        [HideInInspector]
        public PlayerController localPlayer;

        public EnemyController[] enemies;

        // Start is called before the first frame update
        void Start()
        {
            this.player = FindObjectOfType<PlayerController>();
            this.enemies = FindObjectsOfType<EnemyController>();
            // Make sure there is just one GameManager
            if (!GameManager.isGameManagerExists)
            {
                GameManager.isGameManagerExists = true;
                // This code will make sure that the player will not be destroyed when change scenes
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            //PlayerController.RefreshInstance(ref localPlayer, player);
        }

        // private void Awake()
        // {
        //     if (!PhotonNetwork.IsConnected)
        //     {
        //         UnityEngine.SceneManagement.SceneManager.LoadScene("GameMainMenuScene");
        //         return;
        //     }
        // }

        // Update is called once per frame
        void Update()
        {

        }

        // public override void OnPlayerEnteredRoom(Player newPlayer)
        // {
        //     base.OnPlayerEnteredRoom(newPlayer);
        //     PlayerController.RefreshInstance(ref localPlayer, player);
        // }
    }
}


