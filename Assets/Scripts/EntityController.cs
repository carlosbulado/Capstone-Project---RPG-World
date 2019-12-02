using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    protected Rigidbody2D myRigidBody;
    public string startPoint;
    protected bool moving;
    public bool canMove;
    public float wait;
    protected float waitCounter;
    public float move;
    protected float moveCounter;
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
    protected DialogManager dialogManager;

    // Getters
    public StatsManager GetStats() { return this.stats; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.canMove = true;
        this.myRigidBody = GetComponent<Rigidbody2D>();
        this.dialogManager = FindObjectOfType<DialogManager>();

        this.waitCounter = this.wait;
        this.moveCounter = this.move;

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
        if(!this.dialogManager.isActive) { this.canMove = true; }

        if(this.hasWalkZone)
        {
            if(
                (this.moveDirectionNumber == 0 && transform.position.y > this.maxWalkPoint.y)
                || (this.moveDirectionNumber == 1 && transform.position.x > this.maxWalkPoint.x)
                || (this.moveDirectionNumber == 2 && transform.position.y < this.minWalkPoint.y)
                || (this.moveDirectionNumber == 3 && transform.position.x < this.minWalkPoint.x)
            )
            {
                this.StopMoving();
                this.ChooseDirection();
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
        this.waitCounter = this.wait;
    }

    public void ChooseDirection()
    {
        this.moveDirectionNumber = Random.Range(0, 4);
        this.moving = true;
        this.moveCounter = this.move;
    }
}