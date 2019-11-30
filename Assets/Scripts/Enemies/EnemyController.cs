﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    // Variable
    public float moveSpeed;
    protected Rigidbody2D myRigidBody;
    protected bool moving;
    public float timeBetweenMove;
    protected float timeBetweenMoveCounter;
    public float timeToMove;
    protected float timeToMoveCounter;
    protected Vector3 moveDirection;
    protected PlayerController thePlayer;
    
    // Status
    protected StatsManager stats;
    
    // Getters
    public StatsManager GetStats() { return this.stats; }

    // Start is called before the first frame update
    protected void Start()
    {
        this.myRigidBody = GetComponent<Rigidbody2D>();

        this.RenewTimeBetweenMoveCounter();
        this.RenewTimeToMoveCounter();

        this.thePlayer = FindObjectOfType<PlayerController>();

        this.InitStats();
        this.AfterStart();
    }

    // Update is called once per frame
    protected void Update()
    {
        this.Move();
        this.AfterUpdate();
    }

    protected void RenewTimeBetweenMoveCounter()
    {
        this.timeBetweenMoveCounter = Random.Range(this.timeBetweenMove * 0.5f, this.timeBetweenMove * 1.75f);
    }

    protected void RenewTimeToMoveCounter()
    {
        this.timeToMoveCounter = Random.Range(this.timeToMove * 0.5f, this.timeToMove * 1.75f);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            this.Attack();
        }
    }
    
    protected void Attack()
    {
        this.stats.TryAttack(this.thePlayer.GetStats());
    }

    protected abstract void AfterStart();
    protected abstract void AfterUpdate();
    protected abstract void Move();
    protected abstract void InitStats();
}