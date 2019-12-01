using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // Variables
    public QuestObject[] allQuests;
    public bool[] questsCompleted;

    public DialogManager dialogManager;

    // Start is called before the first frame update
    void Start()
    {
        this.questsCompleted = new bool[this.allQuests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestDialog(string[] lines)
    {
        this.dialogManager.ShowDialog(lines);
    }
}
