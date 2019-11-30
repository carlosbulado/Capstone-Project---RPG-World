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
    protected GameObject damageBurst;
    protected GameObject damageNumbers;

    public StatsManager() : base() { }

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
    public int GetMaxHealth() { return this.level * this.strength; }
    public int GetOffense() { return (int)(this.agility * .75); }
    public int GetDefense() { return (int)(this.agility * .75 + this.intelligence * .25); }
    public int GetCurrentHealth() { return this.currentHealth; }

    // Setters
    public void SetLevel(int value) { this.level = value; }
    public void SetAgility(int value) { this.agility = value; }
    public void SetStrength(int value) { this.strength = value; }
    public void SetIntelligence(int value) { this.intelligence = value; }
    public void SetDamageBurst(GameObject value) { this.damageBurst = value; }
    public void SetDamageText(GameObject value) { this.damageNumbers = value; }

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
            break;
            case HitStatus.NotBad:
                damage = this.RollDamage(other);
            break;
            case HitStatus.YoureAwesome:
                damage = this.RollDamage(other);
                damage += this.RollDamage(other);
            break;
        }
        this.ShowDamageBurst(status, damage);
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

    public int RollDamage(StatsManager whoWillTakeDamage)
    {
        int damage = Dice.Roll(this.strength);
        whoWillTakeDamage.GotHit(damage);
        return damage;
    }

    public void GotHit(int damage)
    {
        this.currentHealth -= damage;
        Instantiate(this.damageBurst, this.gameObject.transform.position, this.gameObject.transform.rotation);
        if(this.currentHealth <= 0) Destroy(this.gameObject);
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
            // TODO: Message - "<sprite> epic fail and hit itself by <damage>"
            return this.RollDamage(this);
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
}
