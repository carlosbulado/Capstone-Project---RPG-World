using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using Photon.Pun;
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

    protected Vector2 moveInput;

    // Getters
    public Animator GetAnimator() { return this.animator; }

    // Start is called before the first frame update
    protected override void Start()
    {
        // DB: Read database for this player and get all information about it
        base.Start();
        // this.stats = new StatsManager(1, 1, transform.gameObject, GetComponent<SpriteRenderer>());
        // this.stats.Start();

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

        this.lastMove = new Vector2(0f, -1f);

        base.UpdateObjects();

        // if (!photonView.IsMine && GetComponent<EntityController>() != null)
        // {
        //     Destroy(GetComponent<EnemyController>());
        // }
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
            // Player Movement
            this.moveInput = new Vector2(this.horizontalMovement, this.verticalMovement).normalized;

            if(this.moveInput != Vector2.zero)
            {
                this.myRigidBody.velocity = new Vector2(this.moveInput.x * this.moveSpeed, this.moveInput.y * this.moveSpeed);
                this.moving = true;
                this.lastMove = this.moveInput;
            }
            else
            {
                myRigidBody.velocity = Vector2.zero;
            }

            // Attack Movement
            if(Input.GetKeyDown(KeyCode.J))
            {
                this.attackTimeCounter = this.attackTime;
                this.playerAttacking = true;
                this.myRigidBody.velocity = Vector2.zero;
                this.animator.SetBool("PlayerAttacking", true);
                this.sfxManager.playerAttack.Play();
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

        //this.stats.Update();
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

    public static void RefreshInstance(ref PlayerController player, PlayerController Prefab)
    {
        // if (player != null)
        // {
        //     PhotonNetwork.Destroy(player.gameObject);
        // }

        // player = PhotonNetwork.Instantiate(Prefab.gameObject.name, player.lastMove, Quaternion.identity).GetComponent<PlayerController>();
    }
}

public enum PlayerPosition
{
    Up,
    Down,
    Left,
    Right
}