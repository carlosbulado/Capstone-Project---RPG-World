using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : EnemyController
{
    protected override void AfterStart()
    {
        
    }

    protected override void AfterUpdate()
    {
        
    }

    protected override void Move()
    {
        if(this.moving)
        {
            this.moveCounter -= Time.deltaTime;
            this.myRigidBody.velocity = this.moveDirection;
            if(this.moveCounter < 0f)
            {
                this.moving = false;
                this.RenewmoveCounter();
            }
        }
        else
        {
            this.waitCounter -= Time.deltaTime;
            this.myRigidBody.velocity = Vector2.zero;
            if(this.waitCounter < 0f)
            {
                this.moving = true;
                this.RenewwaitCounter();
                this.moveDirection = new Vector3(Random.Range(-1f, 1f) * this.moveSpeed, Random.Range(-1f, 1f) * this.moveSpeed, 0f);
            }
        }
    }

    // protected override void Attack()
    // {
        
    // }

    protected override void InitStats()
    {
        // Will load something from database?
        this.stats = new StatsManager(1, 3, transform.gameObject, GetComponent<SpriteRenderer>());
        this.stats.Start();
    }
}
