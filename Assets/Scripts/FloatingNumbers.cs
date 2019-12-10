using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumbers : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    protected int damageDone;
    public Text displayNumber;
    protected HitStatus hitStatus;

    // Setters
    public void SetDamageDone(int damage) { this.damageDone = damage; }
    public void SetHitStatus(HitStatus status) { this.hitStatus = status; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.hitStatus)
        {
            case HitStatus.EpicFail:
                this.displayNumber.text = "YOU FAIL MISERABLY !";
                this.displayNumber.color = Color.white;
                break;
            case HitStatus.FacePalm:
                this.displayNumber.text = "MISS !";
                this.displayNumber.color = Color.yellow;
                break;
            case HitStatus.NotBad:
                this.displayNumber.text = "" + this.damageDone;
                this.displayNumber.color = Color.red;
                break;
            case HitStatus.YoureAwesome:
                this.displayNumber.text = "" + this.damageDone + " !!!";
                this.displayNumber.color = Color.magenta;
                break;
            case HitStatus.Recovery:
                this.displayNumber.text = "" + this.damageDone;
                this.displayNumber.color = Color.green;
                break;
            case HitStatus.Invulnerable:
                this.displayNumber.text = "INVULNERABLE !!!";
                this.displayNumber.color = Color.black;
                break;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + (this.moveSpeed * Time.deltaTime), transform.position.z);
    }
}
