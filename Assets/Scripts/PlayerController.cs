using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    // Variables

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
        if(horizontalMovement > 0.5f || horizontalMovement < -0.5f)
        {
            transform.Translate(new Vector3(horizontalMovement * this.moveSpeed * Time.deltaTime, 0f, 0f));
        }
        if(verticalMovement > 0.5f || verticalMovement < -0.5f)
        {
            transform.Translate(new Vector3(0f, verticalMovement * this.moveSpeed * Time.deltaTime, 0f));
        }
    }
}
