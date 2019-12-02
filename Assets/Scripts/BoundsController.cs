using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsController : MonoBehaviour
{
    // Variables
    protected BoxCollider2D bounds;
    protected CameraController camera;

    // Start is called before the first frame update
    void Start()
    {
        this.bounds = GetComponent<BoxCollider2D>();
        this.camera = FindObjectOfType<CameraController>();
        this.camera.SetBounds(this.bounds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
