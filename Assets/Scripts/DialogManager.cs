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

    public string[] dialogLines;
    public int currentLine;
    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        this.thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isActive && Input.GetKeyDown(KeyCode.Space))
        {
            //this.dialogBox.SetActive(false);
            //this.isActive = false;
            this.currentLine++;
        }

        if(this.currentLine >= this.dialogLines.Length)
        {
            this.dialogBox.SetActive(false);
            this.isActive = false;
            this.currentLine = 0;
            this.thePlayer.canMove = true;
        }

        this.dialogText.text = this.dialogLines[this.currentLine];
    }

    public void ShowBox(string message)
    {
        this.isActive = true;
        this.dialogBox.SetActive(true);
        this.dialogText.text = message;
    }

    public void ShowDialog(string[] lines)
    {
        this.dialogLines = lines;
        this.currentLine = 0;
        this.isActive = true;
        this.dialogBox.SetActive(true);
        this.thePlayer.canMove = false;
    }

}
