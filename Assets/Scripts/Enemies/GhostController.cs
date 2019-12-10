using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : EnemyController
{
    public bool flashAfterTakingDamage;
    public float flashAfterTakingDamageLength = 1;
    public float flashAfterTakingDamageCounter = 1;

    public float timeFlashingInit = 8;
    public float timeWithoutFlashingInit = 4;
    protected float timeFlashing = 8;
    protected float timeWithoutFlashing = 4;
    public EnemyController[] allEnemiesOnScreen;

    protected override void AfterStart()
    {
        
    }

    protected override void AfterUpdate()
    {
        if(this.flashAfterTakingDamage)
        {
            if(this.flashAfterTakingDamageCounter > this.flashAfterTakingDamageLength * .66f)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 0f);
            }
            else if(this.flashAfterTakingDamageCounter > this.flashAfterTakingDamageLength * .33f)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 1f);
            }
            else if(this.flashAfterTakingDamageCounter > 0)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 0f);
            }
            else
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 1f);
                this.flashAfterTakingDamageCounter = 1;
            }

            this.flashAfterTakingDamageCounter -= Time.deltaTime;
            
            this.timeFlashing -= Time.deltaTime;
            if(this.timeFlashing <= 0)
            {
                this.spriteRenderer.color = new Color(this.spriteRenderer.color.r, this.spriteRenderer.color.g, this.spriteRenderer.color.b, 1f);
                this.timeFlashing = this.timeFlashingInit;
                this.timeWithoutFlashing = this.timeWithoutFlashingInit;
                this.flashAfterTakingDamage = false;
            }
        }
        else
        {
            this.flashAfterTakingDamageCounter = 1;

            this.timeWithoutFlashing -= Time.deltaTime;
            if(this.timeWithoutFlashing <= 0)
            {
                this.timeFlashing = this.timeFlashingInit;
                this.timeWithoutFlashing = this.timeWithoutFlashingInit;
                this.flashAfterTakingDamage = true;
            }
        }
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
        return !this.flashAfterTakingDamage;
    }
}
