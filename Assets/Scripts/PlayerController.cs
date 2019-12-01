using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    // Variables
    protected Animator animator;
    protected bool playerAttacking;
    public float attackTime;
    protected float attackTimeCounter;
    protected Vector2 lastMove;

    protected float horizontalMovement;
    protected float verticalMovement;

    protected static bool playerExists;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        this.stats = new StatsManager(1, 1, transform.gameObject, GetComponent<SpriteRenderer>());
        this.stats.Start();

        this.animator = GetComponent<Animator>();
        // Fix the duplicates of player in the world
        if(!PlayerController.playerExists)
        {
            PlayerController.playerExists = true;
            // This code will make sure that the player will not be destroyed when change scenes
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        base.UpdateObjects();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Every frame the player movement is false
        this.moving = false;

        if(!this.canMove)
        {
            this.myRigidBody.velocity = Vector2.zero;
            return;
        }

        if(!this.playerAttacking)
        {
            // Get the movement from human player
            this.horizontalMovement = Input.GetAxisRaw("Horizontal");
            this.verticalMovement = Input.GetAxisRaw("Vertical");
            // Last Movement
            // Move the player left, right
            if (this.horizontalMovement > 0.5f || this.horizontalMovement < -0.5f)
            {
                this.MoveHorizontal();
            }
            // Move the player up, down
            if(this.verticalMovement > 0.5f || this.verticalMovement < -0.5f)
            {
                this.MoveVertical();
            }

            // Attack Movement
            if(Input.GetKeyDown(KeyCode.J))
            {
                this.attackTimeCounter = this.attackTime;
                this.playerAttacking = true;
                this.myRigidBody.velocity = Vector2.zero;
                this.animator.SetBool("PlayerAttacking", true);
            }

            if(Mathf.Abs(this.horizontalMovement) > 0.5f && Mathf.Abs(this.verticalMovement) > 0.5f)
            {
                this.currentMoveSpeed = this.moveSpeed * this.diagnoalMoveModifier;
            }
            else
            {
                this.currentMoveSpeed = this.moveSpeed;
            }
        }

        // Control if player is attacking or not
        if(this.attackTimeCounter > 0)
        {
            this.attackTimeCounter -= Time.deltaTime;
        }
        else
        {
            this.playerAttacking = false;
            this.animator.SetBool("PlayerAttacking", false);
        }

        // Stop player from moving forever
        this.StopPlayerIfThereIsNoMovement();
        // Animation
        this.animator.SetFloat("MoveX", this.horizontalMovement);
        this.animator.SetFloat("MoveY", this.verticalMovement);
        this.animator.SetBool("PlayerMoving", this.moving);
        this.animator.SetFloat("LastMoveX", this.lastMove.x);
        this.animator.SetFloat("LastMoveY", this.lastMove.y);

        this.stats.Update();
    }

    void MoveHorizontal()
    {
        //transform.Translate(new Vector3(this.horizontalMovement * this.moveSpeed * Time.deltaTime, 0f, 0f));
        this.myRigidBody.velocity = new Vector2(this.horizontalMovement * this.currentMoveSpeed, this.myRigidBody.velocity.y);
        this.moving = true;
        this.lastMove = new Vector2(this.horizontalMovement, this.verticalMovement);
    }

    void MoveVertical()
    {
        //transform.Translate(new Vector3(0f, this.verticalMovement * this.moveSpeed * Time.deltaTime, 0f));
        this.myRigidBody.velocity = new Vector2(this.myRigidBody.velocity.x, this.verticalMovement * this.currentMoveSpeed);
        this.moving = true;
        this.lastMove = new Vector2(this.horizontalMovement, this.verticalMovement);
    }

    void StopPlayerIfThereIsNoMovement()
    {
        if (this.horizontalMovement < 0.5f && this.horizontalMovement > -0.5f)
        {
            this.myRigidBody.velocity = new Vector2(0f, this.myRigidBody.velocity.y);
        }
        if(this.verticalMovement < 0.5f && this.verticalMovement > -0.5f)
        {
            this.myRigidBody.velocity = new Vector2(this.myRigidBody.velocity.x, 0f);
        }
    }

    public void setPlayerPosition(PlayerPosition position)
    {
        switch(position)
        {
            case PlayerPosition.Up:
                this.lastMove = new Vector2(0f, 1f);
            break;
            case PlayerPosition.Down:
                this.lastMove = new Vector2(0f, 0f);
            break;
            case PlayerPosition.Left:
                this.lastMove = new Vector2(-1f, 0f);
            break;
            case PlayerPosition.Right:
                this.lastMove = new Vector2(1f, 1f);
            break;
        }
    }
}

public enum PlayerPosition
{
    Up,
    Down,
    Left,
    Right
}