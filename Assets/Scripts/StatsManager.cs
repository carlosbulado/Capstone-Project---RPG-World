using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Variables
    protected GameObject gameObject;
    protected int currentHealth;
    protected string name;
    protected int strength;
    protected int agility;
    protected int intelligence;
    protected int level;
    protected int experience;
    protected int minLevel;
    protected int maxLevel;
    protected GameObject damageBurst;
    protected GameObject damageNumbers;
    protected EnemyType enemyType;

    public StatsManager() : base()
    {
        this.name = "Alex Kid";
    }

    public StatsManager(GameObject damageBurst, GameObject damageText) : this()
    {
        this.damageBurst = damageBurst;
        this.damageNumbers = damageNumbers;
    }

    public StatsManager (int minLevel, int maxLevel, GameObject gameObject) : this()
    {
        this.minLevel = minLevel;
        this.maxLevel = maxLevel;
        this.gameObject = gameObject;
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

    // Setters
    public void SetLevel(int value) { this.level = value; }
    public void SetAgility(int value) { this.agility = value; }
    public void SetStrength(int value) { this.strength = value; }
    public void SetIntelligence(int value) { this.intelligence = value; }
    public void SetDamageBurst(GameObject value) { this.damageBurst = value; }
    public void SetDamageText(GameObject value) { this.damageNumbers = value; }
    public void SetEnemyType(EnemyType value) { this.enemyType = value; }
    public void SetName(string value) { this.name = value; }

    // Start is called before the first frame update
    public void Start()
    {
        this.Init();
        this.currentHealth = this.GetMaxHealth();
    }

    // Update is called once per frame
    public void Update()
    {
        if(this.currentHealth < 0)
        {
            gameObject.SetActive(false);
        }

        if(this.gameObject.name == "Player")
        {
            this.TryToLevelUp();
        }
    }

    public void TryAttack(StatsManager other)
    {
        int damage = 0;
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
                damage = this.RollDamage(other);
                UIManager.outputMessages.Add(string.Format("{0} hit {1} by {2}", this.GetName(), other.GetName(), damage));
            break;
            case HitStatus.YoureAwesome:
                damage = this.RollDamage(other);
                damage += this.RollDamage(other);
                UIManager.outputMessages.Add(string.Format("{0} critically hit {1} by {2}", this.GetName(), other.GetName(), damage));
            break;
        }
        this.ShowDamageBurst(status, damage);
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
            if(this.gameObject.tag == "Enemy" && whoHitYou.gameObject.name == "Player")
            {
                whoHitYou.AddExperience(this.ExperienceYeld());
            }
            Destroy(this.gameObject);
        }
    }

    public void RecoverFullHealth()
    {
        this.currentHealth = this.GetMaxHealth();
    }

    public int EpicFail()
    {
        int dice = Dice.Roll(4);
        if(dice == 1)
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
                this.SetStrength(Dice.Roll(8));
                this.SetAgility(Dice.Roll(8));
                this.SetIntelligence(Dice.Roll(8));
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
        this.experience += expToAdd;
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
            UIManager.outputMessages.Add(string.Format("{0} level up! Level {1}", this.GetName(), this.GetLevel()));
        }
    }

    private void LevelUp()
    {
        this.level += 1;
        this.LevelUpStats();
        this.RecoverFullHealth();
    }

    private void LevelUpStats()
    {
        int goUpStats = 2;

        if(this.level % 10 == 0) goUpStats = 8;
        else if(this.level % 5 == 0) goUpStats = 5;
        else if(this.level % 3 == 0) goUpStats = 3;

        int newStr = (Dice.Roll(goUpStats));
        this.strength += newStr;
        int newAgi = (Dice.Roll(goUpStats));
        this.agility += newAgi;
        int newInt = (Dice.Roll(goUpStats));
        this.intelligence += newInt;
        
        UIManager.outputMessages.Add(string.Format("Strength +{0} / Agility +{1} / Intelligence +{2}", newStr, newAgi, newInt));
    }
}
