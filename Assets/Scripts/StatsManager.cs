using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Variables
    public GameObject gameObject;
    public SpriteRenderer spriteRenderer;
    public int currentHealth;
    protected string name;
    public int strength;
    public int agility;
    public int intelligence;
    protected int level;
    protected int experience;
    public int minLevel;
    public int maxLevel;
    public GameObject damageBurst;
    public GameObject damageNumbers;
    protected EnemyType enemyType;

    protected bool flashAfterTakingDamage;
    public float flashAfterTakingDamageLength;
    public float flashAfterTakingDamageCounter;

    public int goldPieces;

    // SFX Manager
    public SFXManager sfxManager;

    public UIManager uiManager;
    public RPGWorldCapstone.SceneManager sceneManager;

    public StatsManager() : base()
    {
        this.name = "Alex Kid";
        this.flashAfterTakingDamageLength = 1;
    }

    public StatsManager(GameObject damageBurst, GameObject damageText) : this()
    {
        this.damageBurst = damageBurst;
        this.damageNumbers = damageNumbers;
    }

    public StatsManager (int minLevel, int maxLevel, GameObject gameObject, SpriteRenderer spriteRenderer) : this()
    {
        this.minLevel = minLevel;
        this.maxLevel = maxLevel;
        this.gameObject = gameObject;
        this.spriteRenderer = spriteRenderer;
    }

    // Getters
    public int GetMaxHealth() { return (int)(this.strength * 1.75); }
    public int GetOffense() { return (int)(this.agility * .75); }
    public int GetDefense() { return (int)(this.agility * .75 + this.intelligence * .25); }
    public int GetCurrentHealth() { return this.currentHealth; }
    public int GetStrength() { return this.strength; }
    public int GetAgility() { return this.agility; }
    public int GetIntelligence() { return this.intelligence; }
    public int GetLevel() { return this.level; }
    public string GetName() { return this.name; }
    public int GetExperience() { return this.experience; }
    public int GetGold() { return this.goldPieces; }

    // Setters
    public void SetLevel(int value) { this.level = value; }
    public void SetAgility(int value) { this.agility = value; }
    public void SetStrength(int value) { this.strength = value; }
    public void SetIntelligence(int value) { this.intelligence = value; }
    public void SetDamageBurst(GameObject value) { this.damageBurst = value; }
    public void SetDamageText(GameObject value) { this.damageNumbers = value; }
    public void SetEnemyType(EnemyType value) { this.enemyType = value; }
    public void SetName(string value) { this.name = value; }
    public void SetFlashLength(float value) { this.flashAfterTakingDamageLength = value; }
    public void SetGold(int value) { this.goldPieces = value; }
    public void SetSpriteRenderer(SpriteRenderer value) { this.spriteRenderer = value; }

    // Start is called before the first frame update
    public void Start()
    {
        this.Init();
        this.currentHealth = this.GetMaxHealth();
        this.sfxManager = FindObjectOfType<SFXManager>();
        if(this.uiManager == null) { this.uiManager = FindObjectOfType<UIManager>(); }
        if(this.sceneManager == null) { this.sceneManager = FindObjectOfType<RPGWorldCapstone.SceneManager>(); }
    }

    // Update is called once per frame
    public void Update()
    {
        if(this.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if(this.gameObject.tag == "Player")
        {
            this.TryToLevelUp();
        }

        if(this.flashAfterTakingDamage)
        {
            if(this.flashAfterTakingDamageCounter > this.flashAfterTakingDamageLength * .66f)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 0f);
            }
            else if(this.flashAfterTakingDamageCounter > this.flashAfterTakingDamageLength * .33f)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 1f);
            }
            else if(this.flashAfterTakingDamageCounter > 0)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 0f);
            }
            else
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 1f);
                this.flashAfterTakingDamage = false;
            }

            this.flashAfterTakingDamageCounter -= Time.deltaTime;
        }
    }

    public void TryAttack(StatsManager other)
    {
        bool canReceiveAttack = true;
        if(other.gameObject.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<EnemyController>();
            canReceiveAttack = enemy.CanReceiveAttack();
        }
        if(canReceiveAttack)
        {
            int damage = 0;
            string messageWhenTryingAttack = string.Empty;
            HitStatus status = this.DidHit(other);
            switch(status)
            {
                case HitStatus.EpicFail:
                    damage = this.EpicFail();
                break;
                case HitStatus.FacePalm:
                    // Missed
                    UIManager.outputMessages.Add(string.Format("{0} missed {1}", this.GetName(), other.GetName()));
                break;
                case HitStatus.NotBad:
                    messageWhenTryingAttack += string.Format("{0} hit {1}", this.GetName(), other.GetName());
                    UIManager.outputMessages.Add(messageWhenTryingAttack);
                    damage = this.RollDamage(other);
                    if(("" + UIManager.outputMessages[UIManager.outputMessages.Count - 1]).Contains("experience"))
                    {
                        UIManager.outputMessages[UIManager.outputMessages.Count - 2] += string.Format(" by {0}", damage);
                    }
                    else
                    {
                        UIManager.outputMessages[UIManager.outputMessages.Count - 1] += string.Format(" by {0}", damage);
                    }
                break;
                case HitStatus.YoureAwesome:
                    messageWhenTryingAttack += string.Format("{0} critically hit {1}", this.GetName(), other.GetName());
                    UIManager.outputMessages.Add(messageWhenTryingAttack);
                    damage = this.RollDamage(other);
                    damage += this.RollDamage(other);
                    if(("" + UIManager.outputMessages[UIManager.outputMessages.Count - 1]).Contains("experience"))
                    {
                        UIManager.outputMessages[UIManager.outputMessages.Count - 2] += string.Format(" by {0}", damage);
                    }
                    else
                    {
                        UIManager.outputMessages[UIManager.outputMessages.Count - 1] += string.Format(" by {0}", damage);
                    }
                break;
            }
            this.ShowDamageBurst(status, damage);
            if(damage > 0)
            {
                other.flashAfterTakingDamage = true;
                other.flashAfterTakingDamageCounter = other.flashAfterTakingDamageLength;
                if(other.gameObject.tag == "Player") { this.sfxManager.playerHurt.Play(); }

                this.sceneManager.SetObjectsThatTouch(this.gameObject, other.gameObject);

                // DB: Save who took damage
            }
        }
        else
        {
            UIManager.outputMessages.Add("INVULNERABLE!");
            var clone = (GameObject)Instantiate(this.damageNumbers, this.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            FloatingNumbers damageText = clone.GetComponent<FloatingNumbers>();
            damageText.SetHitStatus(HitStatus.Invulnerable);
        }
    }

    public HitStatus DidHit(StatsManager other)
    {
        int dice = Dice.Roll(20);
        //UIManager.outputMessages.Add(string.Format("{0} rolled a D{1} - result {2}", this.GetName(), 20, dice));
        switch(dice)
        {
            case 1:
                // Critical failure
                return HitStatus.EpicFail;
            break;
            case 20:
                // Critical hit
                return HitStatus.YoureAwesome;
            break;
            default:
                int offense = dice + this.GetOffense();
                int defense = other.GetDefense();
                return offense > defense ? HitStatus.NotBad : HitStatus.FacePalm;
            break;
        }
    }

    public int RollDamage(StatsManager whoWillTakeDamage)
    {
        int damage = Dice.Roll(this.strength);
        whoWillTakeDamage.GotHit(this, damage);
        return damage;
    }

    public void GotHit(StatsManager whoHitYou, int damage)
    {
        this.currentHealth -= damage;
        Instantiate(this.damageBurst, this.gameObject.transform.position, this.gameObject.transform.rotation);
        if(this.currentHealth <= 0)
        {
            if(this.gameObject.tag == "Enemy" && whoHitYou.gameObject.tag == "Player")
            {
                whoHitYou.AddExperience(this.ExperienceYeld());
            }
        }
    }

    public void RecoverHealth(int quantHeal)
    {
        // DB: Save who fully recovery its HP
        this.currentHealth = this.GetMaxHealth() < this.currentHealth + quantHeal ? this.GetMaxHealth() : this.currentHealth + quantHeal;
        this.ShowDamageBurst(HitStatus.Recovery, quantHeal);
    }

    public void RecoverFullHealth()
    {
        // DB: Save who fully recovery its HP
        this.currentHealth = this.GetMaxHealth();
    }

    public int EpicFail()
    {
        int dice = Dice.Roll(4);
        if(dice % 2 == 0)
        {
            int damage = this.RollDamage(this);
            UIManager.outputMessages.Add(string.Format("{0} roll an epic fail and hit itself by {1} damage", this.GetName(), damage));
            return damage;
        }
        return 0;
    }

    private void Init()
    {
        if(this.level <= 0) 
        {
            int level = Dice.Roll(this.minLevel, this.maxLevel);
            this.SetLevel(level);
            for (int i = 0; i < level; i++)
            {
                //this.SetStrength(Dice.Roll(8));
                //this.SetAgility(Dice.Roll(8));
                //this.SetIntelligence(Dice.Roll(8));
            }
        }
    }

    private void ShowDamageBurst(HitStatus status, int damage = 0)
    {
        var clone = (GameObject)Instantiate(this.damageNumbers, this.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
        FloatingNumbers damageText = clone.GetComponent<FloatingNumbers>();
        damageText.SetDamageDone(damage);
        damageText.SetHitStatus(status);
    }

    public void AddExperience(int expToAdd)
    {
        //this.experience += expToAdd;
        // DB: Add experience for the player
        //UIManager.outputMessages.Add(string.Format("{0} got {1} experience!", this.GetName(), expToAdd));
    }

    public int ExperienceYeld()
    {
        switch(this.enemyType)
        {
            case EnemyType.Slime: return this.level * 2;
        }
        return 1;
    }

    public void TryToLevelUp()
    {
        while (this.experience > this.level * 2)
        {
            this.LevelUp();
        }
    }

    private void LevelUp()
    {
        // DB: Save new level for the player
        //this.level += 1;
        //UIManager.outputMessages.Add(string.Format("{0} level up! Level {1}", this.GetName(), this.GetLevel()));
        //this.LevelUpStats();
        //this.RecoverFullHealth();
    }

    private void LevelUpStats()
    {
        int goUpStats = 2;

        if(this.level % 10 == 0) goUpStats = 8;
        else if(this.level % 5 == 0) goUpStats = 5;
        else if(this.level % 3 == 0) goUpStats = 3;

        //int newStr = (Dice.Roll(goUpStats));
        //this.strength += newStr;
        //int newAgi = (Dice.Roll(goUpStats));
        //this.agility += newAgi;
        //int newInt = (Dice.Roll(goUpStats));
        //this.intelligence += newInt;
        
        // DB: Save new str, agi and int for the player
        //UIManager.outputMessages.Add(string.Format("Strength +{0} / Agility +{1} / Intelligence +{2}", newStr, newAgi, newInt));
    }

    public void AddMoney(int goldToAdd)
    {
        // DB: Save money for the player
        UIManager.outputMessages.Add(string.Format("{0} got {1} gold peices!", this.GetName(), goldToAdd));
        this.goldPieces += goldToAdd;
    }
}
