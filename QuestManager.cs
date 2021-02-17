using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    #region Singleton

    public static QuestManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion

    #region Quest Lists

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public List<Quest> failedQuests;

    #endregion

    #region Class Variables

    

    #endregion

    #region MonoBehaviour

    private void OnEnable()
    {
        onQuestReceived += RecieveQuest;
        onQuestCompleted += CompleteQuest;
        onQuestFailed += FailQuest;
    }

    private void OnDisable()
    {
        onQuestReceived -= RecieveQuest;
        onQuestCompleted -= CompleteQuest;
        onQuestFailed -= FailQuest;
    }

    #endregion

    #region Events

    public delegate void OnReceiveQuest(Quest quest);
    public static OnReceiveQuest onQuestReceived;
    public delegate void OnQuestCompleted(Quest quest);
    public static OnQuestCompleted onQuestCompleted;
    public delegate void OnQuestFailed(Quest quest);
    public static OnQuestFailed onQuestFailed;

    #endregion

    #region Quest Methods
    public void RecieveQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
    }

    public void FailQuest(Quest quest)
    {
        activeQuests.Remove(quest);
        failedQuests.Add(quest);
    }
    
    #endregion

}
