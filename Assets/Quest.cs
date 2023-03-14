using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public enum QuestProgress
    {
        NOT_AVAILABLE,
        AVAILABLE,
        ACCEPTED,
        COMPLETE,
        DONE
    }

    public string title;//标题
    public int id;//id
    public QuestProgress progress;//任务状态
    public string description;//任务介绍
    public string hint;//任务提示，来自发放着
    public string congratulation;//任务完成（对话），来自发放着
    public string summery;//可能是总体任务的简短介绍
    public int nextQuest;//步骤任务？

    public string questObjective;//完成任务需求
    public int questObjectiveCount;//目前任务需求数量
    public int questObjectiveRequirement;//所需任务需求数量

    public int expReward;//经验奖励
    public int goldReward;//金钱奖励
    public string itemReward;//物品奖励
}
