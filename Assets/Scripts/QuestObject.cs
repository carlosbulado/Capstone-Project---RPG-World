using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    // Variables
    public QuestManager questManager;
    public int questNumber;
    public bool isQuestActive;
    protected bool isQuestCompleted;
    public QuestType questType;
    // Dialogs for Quest
    public string[] startQuestDialog;
    public string[] completeQuestDialog;
    public string[] afterCompleteQuestDialog;
    // Item Collection Quest
    public string targetItem;
    // Kill Enemies Quest
    public EnemyType targetEnemyType;
    public int quantEnemiesToKill;
    protected int quantEnemiesKilled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.questType)
        {
            case QuestType.SingleItemCollect:
                if(this.questManager.itemCollected == this.targetItem)
                {
                    this.questManager.itemCollected = string.Empty;
                    this.CompleteQuest();
                }
            break;
            case QuestType.KillEnemies:
                if(this.questManager.enemyKilled == targetEnemyType)
                {
                    this.questManager.enemyKilled = EnemyType.Empty;
                    this.quantEnemiesKilled++;
                }

                if(this.quantEnemiesKilled >= this.quantEnemiesToKill)
                {
                    this.CompleteQuest();
                }
            break;
        }
    }

    public void StartQuest()
    {
        if(!this.isQuestCompleted)
        {
            this.isQuestActive = true;
            this.questManager.ShowQuestDialog(this.startQuestDialog);
        }
    }

    public void CompleteQuest()
    {
        this.questManager.questsCompleted[this.questNumber] = true;
        gameObject.SetActive(false);
        this.isQuestActive = false;
        this.isQuestCompleted = true;
        this.questManager.ShowQuestDialog(this.completeQuestDialog);
    }
}

public enum QuestType
{
    Travelling,
    SingleItemCollect,
    KillEnemies
}