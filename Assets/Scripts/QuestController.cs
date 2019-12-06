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
    public QuestObject quest;
    protected bool isQuestCompleted;

    // Start is called before the first frame update
    void Start()
    {
        this.questManager = FindObjectOfType<QuestManager>();
        this.quest = this.questManager.allQuests[this.questNumber];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.isQuestCompleted = this.questManager.questsCompleted[this.questNumber];
            if(!this.isQuestCompleted)
            {
                if(this.startQuest && !this.quest.isQuestActive && !this.quest.GetIsQuestCompleted())
                {
                    this.quest.gameObject.SetActive(true);
                    this.quest.StartQuest();
                }

                if(this.completeQuest && this.quest.isQuestActive)
                {
                    this.quest.CompleteQuest();
                    this.quest.gameObject.SetActive(false);
                }
            }
        }
    }
}
