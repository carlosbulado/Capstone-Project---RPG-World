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
    public Text goldText;
    public int quantMessages;
    protected PlayerController thePlayer;
    public EnemyController boss;
    public Text enemyHealthText;
    public Slider enemyHealthBar;
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
        
        newText = "Gold: " + this.thePlayer.GetStats().GetGold();
        this.goldText.text = newText;
        
        this.enemyHealthBar.gameObject.SetActive(false);
        if(this.boss != null)
        {
            this.enemyHealthBar.gameObject.SetActive(true);
            this.enemyHealthBar.maxValue = this.boss.GetStats().GetMaxHealth();
            this.enemyHealthBar.value = this.boss.GetStats().GetCurrentHealth();
            
            newText = "HP: " + this.enemyHealthBar.value + "/" + this.enemyHealthBar.maxValue;
            this.enemyHealthText.text = newText;
        }

        string output = "";
        for (int i = 1 ; i < this.quantMessages && UIManager.outputMessages.Count > i ; i++)
        {
            output = UIManager.outputMessages[UIManager.outputMessages.Count - i] + "\n" + output;
        }
        this.gameOutputText.text = output;
    }
}
