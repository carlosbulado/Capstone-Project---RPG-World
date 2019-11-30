using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    protected Rigidbody2D myRigidBody;

    // Stats
    protected StatsManager stats;

    public GameObject damageBurst;
    public GameObject damageText;

    // Getters
    public StatsManager GetStats() { return this.stats; }

    // Start is called before the first frame update
    protected virtual void Start() { }

    // Update is called once per frame
    protected virtual void Update() { }
    
    protected void UpdateObjects()
    {
        this.stats.SetDamageBurst(this.damageBurst);
        this.stats.SetDamageText(this.damageText);
    }
}