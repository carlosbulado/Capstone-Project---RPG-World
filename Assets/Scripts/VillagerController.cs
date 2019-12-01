using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : EntityController
{
    // Variables
    protected int moveDirection;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.ChooseDirection();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(this.moving)
        {
            this.timeToMoveCounter -= Time.deltaTime;

            switch(this.moveDirection)
            {
                case 0:
                    this.myRigidBody.velocity = new Vector2(0f, this.moveSpeed);
                break;
                case 1:
                    this.myRigidBody.velocity = new Vector2(this.moveSpeed, 0f);
                break;
                case 2:
                    this.myRigidBody.velocity = new Vector2(-this.moveSpeed, 0f);
                break;
                case 3:
                    this.myRigidBody.velocity = new Vector2(0f, -this.moveSpeed);
                break;
            }
            
            if(this.timeToMoveCounter < 0)
            {
                this.moving = false;
                this.timeBetweenMoveCounter = this.timeBetweenMove;
            }
        }
        else
        {
            this.timeBetweenMoveCounter -= Time.deltaTime;
            this.myRigidBody.velocity = Vector2.zero;
            if(this.timeBetweenMoveCounter < 0)
            {
                this.ChooseDirection();
                this.timeBetweenMoveCounter = this.timeBetweenMove;
            }
        }
    }

    public void ChooseDirection()
    {
        this.moveDirection = Random.Range(0, 4);
        this.moving = true;
        this.timeToMoveCounter = this.timeToMove;
    }
}
