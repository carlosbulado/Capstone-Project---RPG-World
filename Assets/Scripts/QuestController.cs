using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    // Variables
    protected QuestManager questManager;
    public int questNumber;
    public bool startQuest;
    public bool completeQuest;
    protected QuestObject thisQuest;
    protected bool isThisQuestCompleted;

    // Start is called before the first frame update
    void Start()
    {
        this.questManager = FindObjectOfType<QuestManager>();
        this.thisQuest = this.questManager.allQuests[this.questNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            
            bool isThisQuestCompleted = this.questManager.questsCompleted[this.questNumber];
            if(!isThisQuestCompleted)
            {
                if(this.startQuest && !this.thisQuest.gameObject.activeSelf)
                {
                    this.thisQuest.gameObject.SetActive(true);
                    this.thisQuest.StartQuest();
                }

                if(this.completeQuest && this.thisQuest.gameObject.activeSelf)
                {
                    this.thisQuest.CompleteQuest();
                    this.thisQuest.gameObject.SetActive(false);
                }
            }
        }
    }
}
