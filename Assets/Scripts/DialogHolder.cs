using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
{
    // Variables
    public string message;
    private DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        this.dialogManager = FindObjectOfType<DialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if(Input.GetKeyUp(KeyCode.Space))
            {
                this.dialogManager.ShowBox(this.message);
            }
        }
    }
}
