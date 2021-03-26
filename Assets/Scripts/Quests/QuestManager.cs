using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questPrefab;

    private ArrayList activeQuests;

    public void Start() 
    {
        activeQuests = new ArrayList();
    }

    /// <summary>
    /// Add a quest to the manager.
    /// </summary>
    /// <param name="questName">
    /// Name of the Quest Detail Scriptable Object.
    /// </param>
    public void AddQuest(string questName)
    {
        QuestDetail detail = Resources.Load<QuestDetail>("Quests/" + questName);
        QuestUI quest = Instantiate(questPrefab, transform).GetComponent<QuestUI>();
        quest.StartQuest(detail);
        activeQuests.Add(quest);
        ToggleQuestItemInteractable(detail);
    }

    private void ToggleQuestItemInteractable(QuestDetail quest)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(PickupItem.DEFAULT_TAG);
        foreach (GameObject gameObj in objects) 
        {
            PickupItem item = gameObj.GetComponent<PickupItem>();
            if (item.itemName == quest.itemName)
            {
                item.BecomeInteractible();
            }
        }
    }

    /// <summary>
    /// Wrapper for the AddQuest(string) method. To be used as a
    /// callback for when a dialogue ends.
    /// </summary>
    /// <param name="srcObject">
    /// The object that called this callback.
    /// </param>
    ///  <param name="args">
    /// The event handler object containing the questName.
    /// </param>
    public void AddQuest(object srcObject, DialogueEndedEventArgs args)
    {
        AddQuest(args.questName);
    }

    /// <summary>
    /// Check whether the itemName belongs to any
    /// of the quest. If it is, update the quest progress.
    /// </summary>
    /// <param name="itemName">
    /// Name of an Item Prefab.
    /// </param>
    /// <returns>
    /// Whether the quest item is removed from the 
    /// scene.
    /// </returns>
    public bool CheckQuestItem(string itemName)
    {
        QuestUI finishedQuest = null;
        foreach (QuestUI quest in activeQuests)
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
            return true;
        }
        return false;
    }

}
