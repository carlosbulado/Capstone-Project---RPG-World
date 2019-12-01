using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    // Variables
    public QuestManager questManager;
    public int questNumber;

    public string[] startQuestDialog;
    public string[] completeQuestDialog;
    public string[] afterCompleteQuestDialog;

    public bool isItemQuest;
    public string targetItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.isItemQuest)
        {
            if(this.questManager.itemCollected == this.targetItem)
            {
                this.questManager.itemCollected = string.Empty;
                this.CompleteQuest();
            }
        }
    }

    public void StartQuest()
    {
        this.questManager.ShowQuestDialog(this.startQuestDialog);
    }

    public void CompleteQuest()
    {
        this.questManager.questsCompleted[this.questNumber] = true;
        gameObject.SetActive(false);
        this.questManager.ShowQuestDialog(this.completeQuestDialog);
        //this.questManager.CompleteQuest();
    }
}
