using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    // Variables
    public int questNumber;
    protected QuestManager questManager;
    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        this.questManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if(!this.questManager.questsCompleted[this.questNumber] && this.questManager.allQuests[this.questNumber].gameObject.activeSelf)
            {
                this.questManager.itemCollected = this.itemName;
                gameObject.SetActive(false);
            }
        }
    }

}
