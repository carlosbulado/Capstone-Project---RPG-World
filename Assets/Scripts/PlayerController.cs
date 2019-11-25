using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    private Animator animator;
    // Variables

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the movement from human player
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
        // Move the player left, right
        if(horizontalMovement > 0.5f || horizontalMovement < -0.5f)
        {
            transform.Translate(new Vector3(horizontalMovement * this.moveSpeed * Time.deltaTime, 0f, 0f));
        }
        // Move the player up, down
        if(verticalMovement > 0.5f || verticalMovement < -0.5f)
        {
            transform.Translate(new Vector3(0f, verticalMovement * this.moveSpeed * Time.deltaTime, 0f));
        }
        // Animation
        this.animator.SetFloat("MoveX", horizontalMovement);
        this.animator.SetFloat("MoveY", verticalMovement);
    }
}
