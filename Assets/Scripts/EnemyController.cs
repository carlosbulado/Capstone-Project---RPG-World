using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variable
    public EnemyType type;
    public float moveSpeed;
    private Rigidbody2D myRigidBody;
    private bool moving;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        this.myRigidBody = GetComponent<Rigidbody2D>();
        this.timeBetweenMoveCounter = this.timeBetweenMove;
        this.timeToMoveCounter = this.timeToMove;
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.type)
        {
            case EnemyType.Slime:
                this.MoveSlime();
            break;
        }
    }

    void MoveSlime()
    {
        if(this.moving)
        {
            this.timeToMoveCounter -= Time.deltaTime;
            this.myRigidBody.velocity = this.moveDirection;
            if(this.timeToMoveCounter < 0f)
            {
                this.moving = false;
                this.timeToMoveCounter = this.timeToMove;
            }
        }
        else
        {
            this.timeBetweenMoveCounter -= Time.deltaTime;
            this.myRigidBody.velocity = Vector2.zero;
            if(this.timeBetweenMoveCounter < 0f)
            {
                this.moving = true;
                this.timeBetweenMoveCounter = this.timeBetweenMove;
                this.moveDirection = new Vector3(Random.Range(-1f, 1f) * this.moveSpeed, Random.Range(-1f, 1f) * this.moveSpeed, 0f);
            }
        }
    }
}

public enum EnemyType
{
    Slime
}