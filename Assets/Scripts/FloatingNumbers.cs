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
            break;
            case HitStatus.FacePalm:
                this.displayNumber.text = "MISS !";
            break;
            case HitStatus.NotBad:
                this.displayNumber.text = "" + this.damageDone;
            break;
            case HitStatus.YoureAwesome:
                this.displayNumber.text = "" + this.damageDone + " !!!";
            break;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y + (this.moveSpeed * Time.deltaTime), transform.position.z);
    }
}
