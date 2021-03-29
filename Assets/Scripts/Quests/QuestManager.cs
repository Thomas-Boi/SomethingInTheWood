using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject progressQuestPrefab;
    public GameObject talkQuestPrefab;

    private ArrayList activeQuests;

    // margin for the each quest ui on canvas
    const int Y_OFFSET = -20;

    public void Awake() 
    {
        activeQuests = new ArrayList();
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
    /// Add a quest to the manager.
    /// </summary>
    /// <param name="questName">
    /// Name of the Quest Detail Scriptable Object.
    /// </param>
    public void AddQuest(string questName)
    {
        QuestDetail detail = Resources.Load<QuestDetail>("Quests/" + questName);
        QuestUI quest;
        switch(detail.questType)
        {
            case QuestType.TALK:
                quest = Instantiate(talkQuestPrefab, transform).GetComponent<QuestUI>();
                break;
            default:
                quest = Instantiate(progressQuestPrefab, transform).GetComponent<QuestUI>();
                break;
        }
        quest.StartQuest(detail);
        activeQuests.Add(quest);
        DisplayQuests();
        ToggleQuestItemInteractable(detail);
    }

    // make all the quest item associated with this quest toggeable
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
    /// Display the quests on the scene to ensure they are laid out right.
    /// This should be called after the activeQuests is changed.
    /// </summary>
    private void DisplayQuests()
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            RectTransform questTransform = ((QuestUI) activeQuests[i]).GetComponent<RectTransform>();
            Vector2 newPos = new Vector2(questTransform.anchoredPosition.x, i * Y_OFFSET);
            questTransform.anchoredPosition = newPos;
        }
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
            if (quest.CheckObject(itemName))
            {
                bool finished = quest.UpdateProgress();
                if (finished)
                {
                    finishedQuest = quest; // can't delete while looping
                    break;
                }
            }
        }

        if (finishedQuest != null)
        {
            activeQuests.Remove(finishedQuest);
            if (finishedQuest.detail.nextQuestName != "")
            {
                AddQuest(finishedQuest.detail.nextQuestName);
            }
            DisplayQuests();
            return true;
        }
        return false;
    }

}
