using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Variables
    public Slider healthBar;
    public Text healthText;
    public Text strText;
    public Text agiText;
    public Text intText;
    public Text levelText;
    public Text playerNameText;
    public Text gameOutputText;
    protected PlayerController thePlayer;
    private static bool uiExists;
    public static ArrayList outputMessages = new ArrayList();

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

        string newText = "HP: " + this.healthBar.value + "/" + this.healthBar.maxValue;
        this.healthText.text = newText;
        
        newText = "STR: " + this.thePlayer.GetStats().GetStrength();
        this.strText.text = newText;
        
        newText = "AGI: " + this.thePlayer.GetStats().GetAgility();
        this.agiText.text = newText;
        
        newText = "INT: " + this.thePlayer.GetStats().GetIntelligence();
        this.intText.text = newText;
        
        newText = "Level: " + this.thePlayer.GetStats().GetLevel();
        this.levelText.text = newText;
        
        newText = "" + this.thePlayer.GetStats().GetName();
        this.playerNameText.text = newText;

        string output = "";
        for (int i = 1 ; i < 5 && UIManager.outputMessages.Count > i ; i++)
        {
            output = UIManager.outputMessages[UIManager.outputMessages.Count - i] + "\n" + output;
        }
        this.gameOutputText.text = output;
    }
}
