using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPageController : MonoBehaviour
{
    // Variables
    public InputField usernameInput;
    public InputField passwordInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Login()
    {
        //  DB: Needs to try username and password against the database and return
        //      if account exists and if the username password is correct
        return true;
    }
}
