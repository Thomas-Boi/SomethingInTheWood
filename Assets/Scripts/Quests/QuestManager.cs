using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    private ArrayList activeQuests;

    public GameObject questPrefab;

    public void Start() 
    {
        activeQuests = new ArrayList();
    }

    /// <summary>
    /// Addd a quest to the manager.
    /// </summary>
    /// <param name="questDetailName">
    /// Name of the Quest Detail Scriptable Object.
    /// </param>
    public void AddQuest(string questDetailName)
    {
        QuestDetail detail = Resources.Load<QuestDetail>("Quests/" + questDetailName);
        Quest quest = Instantiate(questPrefab, transform).GetComponent<Quest>();
        quest.StartQuest(detail);
        activeQuests.Add(quest);
    }

    /// <summary>
    /// Check whether the itemName belongs to any
    /// of the quest. If it is, update the quest progress.
    /// </summary>
    /// <param name="itemName"></param>
    public void CheckQuestItem(string itemName)
    {
        Quest finishedQuest = null;
        foreach (Quest quest in activeQuests)
        {
            if (quest.CheckItem(itemName))
            {
                bool finished = quest.UpdateProgress();
                if (finished)
                {
                    finishedQuest = quest;
                    break;
                }
            }
        }

        if (finishedQuest != null)
        {
            activeQuests.Remove(finishedQuest);
        }
    }

}
