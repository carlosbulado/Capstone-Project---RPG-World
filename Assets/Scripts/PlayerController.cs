using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    private Animator animator;
    private bool playerMoving;
    private Vector2 lastMove;

    private float horizontalMovement;
    private float verticalMovement;
    // Variables

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
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
        // Animation
        this.animator.SetFloat("MoveX", this.horizontalMovement);
        this.animator.SetFloat("MoveY", this.verticalMovement);
        this.animator.SetBool("PlayerMoving", this.playerMoving);
        this.animator.SetFloat("LastMoveX", this.lastMove.x);
        this.animator.SetFloat("LastMoveY", this.lastMove.y);
    }

    void MoveHorizontal()
    {
        transform.Translate(new Vector3(this.horizontalMovement * this.moveSpeed * Time.deltaTime, 0f, 0f));
        this.playerMoving = true;
        this.lastMove = new Vector2(this.horizontalMovement, this.verticalMovement);
    }

    void MoveVertical()
    {
        transform.Translate(new Vector3(0f, this.verticalMovement * this.moveSpeed * Time.deltaTime, 0f));
        this.playerMoving = true;
        this.lastMove = new Vector2(this.horizontalMovement, this.verticalMovement);
    }
}
