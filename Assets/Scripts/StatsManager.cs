using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Variables
    protected GameObject gameObject;
    protected int currentHealth;
    protected int strength;
    protected int agility;
    protected int intelligence;
    protected int level;
    protected int minLevel;
    protected int maxLevel;

    public StatsManager() : base() { }
    public StatsManager (int minLevel, int maxLevel, GameObject gameObject) : base()
    {
        this.minLevel = minLevel;
        this.maxLevel = maxLevel;
        this.gameObject = gameObject;
    }

    // Getters
    public int GetMaxHealth() { return this.level * this.strength; }
    public int GetOffense() { return (int)(this.agility * .75); }
    public int GetDefense() { return (int)(this.agility * .75 + this.intelligence * .25); }

    // Setters
    public void SetLevel(int value) { this.level = value; }
    public void SetAgility(int value) { this.agility = value; }
    public void SetStrength(int value) { this.strength = value; }
    public void SetIntelligence(int value) { this.intelligence = value; }

    // Start is called before the first frame update
    public void Start()
    {
        if(this.level <= 0) this.Init();
        this.currentHealth = this.GetMaxHealth();
    }

    // Update is called once per frame
    public void Update()
    {
        if(this.currentHealth < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void TryAttack(StatsManager other)
    {
        switch(this.DidHit(other))
        {
            case HitStatus.EpicFail:
                this.EpicFail();
            break;
            case HitStatus.FacePalm:
                // Missed
            break;
            case HitStatus.NotBad:
                this.RollDamage(other);
            break;
            case HitStatus.YoureAwesome:
                this.RollDamage(other);
                this.RollDamage(other);
            break;
        }
    }

    public HitStatus DidHit(StatsManager other)
    {
        int dice = Dice.Roll(20);
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

    public void RollDamage(StatsManager whoWillTakeDamage)
    {
        int damage = Dice.Roll(this.strength);
        whoWillTakeDamage.GotHit(damage);
    }

    public void GotHit(int damage)
    {
        this.currentHealth -= damage;
        if(this.currentHealth <= 0) Destroy(this.gameObject);
    }

    public void RecoverFullHealth()
    {
        this.currentHealth = this.GetMaxHealth();
    }

    public void EpicFail()
    {
        int dice = Dice.Roll(4);
        if(dice == 1)
        {
            // TODO: Message - "<sprite> epic fail and hit itself by <damage>"
            this.RollDamage(this);
        }
    }

    private void Init()
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
