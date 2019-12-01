using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : EntityController
{
    // Variables

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

        if(!this.canMove)
        {
            this.myRigidBody.velocity = Vector2.zero;
            return;
        }

        if(this.moving)
        {
            this.moveCounter -= Time.deltaTime;

            switch(this.moveDirectionNumber)
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
            
            if(this.moveCounter < 0) { this.StopMoving(); }
        }
        else
        {
            this.waitCounter -= Time.deltaTime;
            this.myRigidBody.velocity = Vector2.zero;
            if(this.waitCounter < 0)
            {
                this.ChooseDirection();
            }
        }
    }
}
