﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    private Animator animator;
    private Rigidbody2D playerRigidBody;
    private bool playerMoving;
    private Vector2 lastMove;

    private float horizontalMovement;
    private float verticalMovement;

    private static bool playerExists;

    // Status
    private float health;
    private int strength;
    private int agility;
    private int intelligence;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.playerRigidBody = GetComponent<Rigidbody2D>();
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
    }

    // Update is called once per frame
    void Update()
    {
        // Every frame the player movement is false
        this.playerMoving = false;
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
        // Stop player from moving forever
        this.StopPlayerIfThereIsNoMovement();
        // Animation
        this.animator.SetFloat("MoveX", this.horizontalMovement);
        this.animator.SetFloat("MoveY", this.verticalMovement);
        this.animator.SetBool("PlayerMoving", this.playerMoving);
        this.animator.SetFloat("LastMoveX", this.lastMove.x);
        this.animator.SetFloat("LastMoveY", this.lastMove.y);
    }

    void MoveHorizontal()
    {
        //transform.Translate(new Vector3(this.horizontalMovement * this.moveSpeed * Time.deltaTime, 0f, 0f));
        this.playerRigidBody.velocity = new Vector2(this.horizontalMovement * this.moveSpeed, this.playerRigidBody.velocity.y);
        this.playerMoving = true;
        this.lastMove = new Vector2(this.horizontalMovement, this.verticalMovement);
    }

    void MoveVertical()
    {
        //transform.Translate(new Vector3(0f, this.verticalMovement * this.moveSpeed * Time.deltaTime, 0f));
        this.playerRigidBody.velocity = new Vector2(this.playerRigidBody.velocity.x, this.verticalMovement * this.moveSpeed);
        this.playerMoving = true;
        this.lastMove = new Vector2(this.horizontalMovement, this.verticalMovement);
    }

    void StopPlayerIfThereIsNoMovement()
    {
        if (this.horizontalMovement < 0.5f && this.horizontalMovement > -0.5f)
        {
            this.playerRigidBody.velocity = new Vector2(0f, this.playerRigidBody.velocity.y);
        }
        if(this.verticalMovement < 0.5f && this.verticalMovement > -0.5f)
        {
            this.playerRigidBody.velocity = new Vector2(this.playerRigidBody.velocity.x, 0f);
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