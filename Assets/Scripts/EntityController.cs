using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    protected float currentMoveSpeed;
    public float diagnoalMoveModifier;
    protected Rigidbody2D myRigidBody;
    public string startPoint;
    protected bool moving;
    public float timeBetweenMove;
    protected float timeBetweenMoveCounter;
    public float timeToMove;
    protected float timeToMoveCounter;
    protected Vector3 moveDirection;

    // Stats
    protected StatsManager stats;

    public GameObject damageBurst;
    public GameObject damageText;

    // Getters
    public StatsManager GetStats() { return this.stats; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.myRigidBody = GetComponent<Rigidbody2D>();

        this.timeBetweenMoveCounter = this.timeBetweenMove;
        this.timeToMoveCounter = this.timeToMove;
    }

    // Update is called once per frame
    protected virtual void Update() { }
    
    protected void UpdateObjects()
    {
        this.stats.SetDamageBurst(this.damageBurst);
        this.stats.SetDamageText(this.damageText);
    }
}