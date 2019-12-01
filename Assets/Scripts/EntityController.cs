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
    protected int moveDirectionNumber;
    protected Vector3 moveDirection;
    protected bool hasWalkZone;
    public Collider2D walkZone;
    protected Vector2 minWalkPoint;
    protected Vector2 maxWalkPoint;

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

        if(this.walkZone != null)
        {
            this.hasWalkZone = true;
            this.minWalkPoint = this.walkZone.bounds.min;
            this.maxWalkPoint = this.walkZone.bounds.max;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(this.hasWalkZone)
        {
            if(transform.position.y > this.maxWalkPoint.y 
            || transform.position.x > this.maxWalkPoint.x
            || transform.position.y < this.minWalkPoint.y
            || transform.position.x < this.minWalkPoint.x)
            {
                this.StopMoving();
            }
        }
    }
    
    protected void UpdateObjects()
    {
        this.stats.SetDamageBurst(this.damageBurst);
        this.stats.SetDamageText(this.damageText);
    }

    public void StopMoving()
    {
        this.moving = false;
        this.timeBetweenMoveCounter = this.timeBetweenMove;
        this.ChooseDirection();
    }

    public void ChooseDirection()
    {
        this.moveDirectionNumber = Random.Range(0, 4);
        this.moving = true;
        this.timeToMoveCounter = this.timeToMove;
    }
}