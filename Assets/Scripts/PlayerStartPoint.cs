using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    // Variables
    private PlayerController thePlayer;
    private CameraController theCamera;
    public PlayerPosition position;
    public string pointName;

    // Start is called before the first frame update
    void Start()
    {
        this.thePlayer = FindObjectOfType<PlayerController>();

        if(this.thePlayer.startPoint == this.pointName)
        {
            this.thePlayer.transform.position = transform.position;
            this.thePlayer.setPlayerPosition(this.position);

            this.theCamera = FindObjectOfType<CameraController>();
            this.theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, this.theCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
