using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variables
    public Slider healthBar;
    public Text healthText;
    protected PlayerController thePlayer;
    private static bool uiExists;

    // Start is called before the first frame update
    void Start()
    {
        this.thePlayer = FindObjectOfType<PlayerController>();
        // Fix the duplicates of player in the world
        if(!UIManager.uiExists)
        {
            UIManager.uiExists = true;
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
        this.healthBar.maxValue = this.thePlayer.GetStats().GetMaxHealth();
        this.healthBar.value = this.thePlayer.GetStats().GetCurrentHealth();

        string hpText = "HP: " + this.healthBar.value + "/" + this.healthBar.maxValue;
        this.healthText.text = hpText;
    }
}
