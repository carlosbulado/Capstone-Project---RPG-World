using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    // Variables
    public GameObject dialogBox;
    public Text dialogText;
    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isActive && Input.GetKeyDown(KeyCode.Space))
        {
            this.dialogBox.SetActive(false);
        }
    }

    public void ShowBox(string message)
    {
        this.isActive = true;
        this.dialogBox.SetActive(true);
        this.dialogText.text = message;
    }
}
