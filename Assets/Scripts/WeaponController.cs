using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Variables
    protected PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            this.player.GetStats().TryAttack(enemy.GetStats());
        }
    }
}
