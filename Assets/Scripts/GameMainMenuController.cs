using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMainMenuController : MonoBehaviour
{
    // Variables
    public bool isPlayerLogged;
    public LoginPageController loginPageController;
    public LoggedPageController loggedPageController;


    // Start is called before the first frame update
    void Start()
    {
        if(this.loginPageController == null) this.loginPageController = FindObjectOfType<LoginPageController>();
        if(this.loggedPageController == null) this.loggedPageController = FindObjectOfType<LoggedPageController>();
        this.CheckPlayerLogged();
    }

    // Update is called once per frame
    void Update()
    {
        //this.CheckPlayerLogged();
        FindObjectOfType<PlayerController>().HidePlayer();
    }

    public void CheckPlayerLogged()
    {
        if(this.loginPageController != null && this.loggedPageController != null)
        {
            if(this.isPlayerLogged)
            {
                this.loginPageController.gameObject.SetActive(false);
                this.loggedPageController.gameObject.SetActive(true);
            }
            else
            {
                this.loginPageController.gameObject.SetActive(true);
                this.loggedPageController.gameObject.SetActive(false);
            }
        }
    }

    public void TryLogin()
    {
        //this.isPlayerLogged = this.loginPageController.Login();
        //if(!this.isPlayerLogged)
        //{
        //    // Message like 'Username or Password Incorrect'
        //}
        //this.CheckPlayerLogged();
        Application.LoadLevel("Level 1");
        try
        {
            FindObjectOfType<PlayerController>().gameObject.SetActive(true);
            FindObjectOfType<PlayerController>().startPoint = "Level_1_SP";
        }
        catch { }
    }

    public void TryLogout()
    {
        this.isPlayerLogged = false;
        this.CheckPlayerLogged();
    }

    public void TryCreateNewCharacter()
    {
        // Delete the previous character
        var loadGame = FindObjectOfType<LoadNewArea>();
        loadGame.GoTo();
    }
}
