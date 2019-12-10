using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : EnemyController
{
    public EnemyController[] allEnemiesOnScreen;
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
            this.MovementFollowingThePlayer();
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
            }
        }
    }

    public override bool CanReceiveAttack()
    {
        this.allEnemiesOnScreen = FindObjectsOfType<EnemyController>();
        return this.allEnemiesOnScreen.Length <= 2;
    }

    // protected override void Attack()
    // {
        
    // }

    // protected override void InitStats()
    // {
    //     // Will load something from database?
    //     this.stats = new StatsManager(1, 3, transform.gameObject, GetComponent<SpriteRenderer>());
    //     this.stats.Start();
    // }
}
