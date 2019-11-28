using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Variables
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;
    //Variables

    // Start is called before the first frame update
    void Start()
    {
        // This code will make sure that the camera will not be destroyed when change scenes
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, this.targetPosition, this.moveSpeed * Time.deltaTime);
    }
}
