using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class EntityController : MonoBehaviourPun
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
    public StatsManager stats;

    public GameObject damageBurst;
    public GameObject damageText;
    public DialogManager dialogManager;

    // SFX Manager
    public SFXManager sfxManager;

    // Getters
    public StatsManager GetStats() { return this.stats; }
    public bool IsMoving() { return this.moving; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.canMove = true;
        
        this.InitStats();
        this.myRigidBody = GetComponent<Rigidbody2D>();
        this.dialogManager = FindObjectOfType<DialogManager>();
        this.sfxManager = FindObjectOfType<SFXManager>();

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

    public void FixMovementWithWalkZone()
    {
        if(this.hasWalkZone)
        {
            if(
                (transform.position.y > this.maxWalkPoint.y)
                || (transform.position.x > this.maxWalkPoint.x)
                || (transform.position.y < this.minWalkPoint.y)
                || (transform.position.x < this.minWalkPoint.x)
            )
            {
                this.StopMoving();
                this.ChooseDirection();
            }
        }
    }

    protected void InitStats()
    {
        this.stats = GetComponent<StatsManager>();
        this.stats.SetSpriteRenderer(GetComponent<SpriteRenderer>());
    }
}