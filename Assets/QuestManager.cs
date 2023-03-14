using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager:MonoBehaviour
{
    public static QuestManager questManager;
    public List<Quest> questList = new List<Quest>();//所有任务
    public List<Quest> currentQuestList = new List<Quest>();//目前已接受任务

    private void Awake()
    {
        if (questManager == null) questManager = this;
        else if(questManager != this)Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    //从任务媒介获取可接受任务
    public void QuestRequest(QuestObject questObject)
    {
        if (questObject.availbleQuestIDs.Count <= 0) return;
        //获取任务
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questObject.availbleQuestIDs.Count; j++)
            {
                if (questList[i].id == questObject.availbleQuestIDs[j]
                    && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    Debug.Log("Quest ID:" + questObject.availbleQuestIDs[j] + "" + questList[i].progress);
                    AcceptQuest(questObject.availbleQuestIDs[j]);
                    //quest ui manager
                }
            }
        }
        //提交任务
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            for (int j = 0; j < questObject.receivableQuestIDs.Count; j++)
            {
                if (currentQuestList[i].id == questObject.receivableQuestIDs[j]
                    && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED
                    || currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    Debug.Log("QuestID:" + questObject.receivableQuestIDs[j] + "is" + currentQuestList[i].progress);
                    CompleteQuest(questObject.receivableQuestIDs[j]);
                }
            }
        }
    }
    //接受任务
    public void AcceptQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID
                && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                currentQuestList.Add(questList[i]);
                questList[i].progress = Quest.QuestProgress.ACCEPTED;
            }
        }
        
    }
    //放弃任务
    public void GiveUpQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID 
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.AVAILABLE;
                currentQuestList[i].questObjectiveCount = 0;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
    }
    //完成任务
    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            if (currentQuestList[i].id == questID && currentQuestList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                currentQuestList[i].progress = Quest.QuestProgress.DONE;
                currentQuestList.Remove(currentQuestList[i]);
            }
        }
        CheckChainQuest(questID);
    }
    //查找任务链
    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }

        if (tempID > 0)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].id == tempID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    questList[i].progress = Quest.QuestProgress.AVAILABLE;
                }
            }
        }
    }
    //添加任务需求值
    public void AddQuestItem(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuestList.Count; i++)
        {
            //添加需求数量到目前已接受任务列表
            if (currentQuestList[i].questObjective == questObjective 
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].questObjectiveCount += itemAmount;
            }
            //如果数量达标，则更换任务进展状态
            if (currentQuestList[i].questObjectiveCount >= currentQuestList[i].questObjectiveRequirement 
                && currentQuestList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                currentQuestList[i].progress = Quest.QuestProgress.COMPLETE;
            }
        }
    }

    //bool1 根据任务id返回任务是否为某个状态
    public bool RequestAvailabledQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.AVAILABLE)
            {
                return true;
            }
        }
        return false;
    }
    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.COMPLETE)
            {
                return true;
            }
        }
        return false;
    }
    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.QuestProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }
    
    //bool2 根据任务媒介返回是否有某个状态的任务
    public bool CheckAvailableQuest(QuestObject questObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questObject.availbleQuestIDs.Count; j++)
            {
                if (questList[i].id == questObject.availbleQuestIDs[j]
                    && questList[i].progress == Quest.QuestProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public bool CheckAcceptedQuest(QuestObject questObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == questObject.receivableQuestIDs[j]
                    && questList[i].progress == Quest.QuestProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public bool CheckCompletedQuest(QuestObject questObject)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questObject.receivableQuestIDs.Count; j++)
            {
                if (questList[i].id == questObject.receivableQuestIDs[j]
                    && questList[i].progress == Quest.QuestProgress.COMPLETE)
                {
                    return true;
                }
            }
        }

        return false;
    }



}

