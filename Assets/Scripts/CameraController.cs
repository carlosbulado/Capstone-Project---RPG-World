using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Variables
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;

    private static bool cameraExists;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Camera camera;
    private float halfHeight;
    private float halfWidth;
    //Variables

    // Start is called before the first frame update
    void Start()
    {
        // Fix the duplicates of cameras in the world
        if(!CameraController.cameraExists)
        {
            CameraController.cameraExists = true;
            // This code will make sure that the camera will not be destroyed when change scenes
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if(this.boundBox == null)
        {
            this.boundBox = FindObjectOfType<BoundsController>().GetComponent<BoxCollider2D>();
        }
        this.SetBounds(this.boundBox);

        this.camera = GetComponent<Camera>();
        this.halfHeight = this.camera.orthographicSize;
        this.halfWidth = this.halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        this.targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, this.targetPosition, this.moveSpeed * Time.deltaTime);

        if(this.boundBox == null)
        {
            this.boundBox = FindObjectOfType<BoundsController>().GetComponent<BoxCollider2D>();
            this.SetBounds(this.boundBox);
        }

        float clampedX = Mathf.Clamp(transform.position.x, this.minBounds.x + this.halfWidth, this.maxBounds.x - this.halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, this.minBounds.y + this.halfHeight, this.maxBounds.y - this.halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void SetBounds(BoxCollider2D newBounds)
    {
        this.boundBox = newBounds;

        this.minBounds = this.boundBox.bounds.min;
        this.maxBounds = this.boundBox.bounds.max;
    }
}
