using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务媒介，如NPC
/// </summary>
public class QuestObject:MonoBehaviour
{
    private bool inTrigger = false;
    
    public List<int> availbleQuestIDs = new List<int>();//可接受任务列表
    public List<int> receivableQuestIDs = new List<int>();//可提交任务列表

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            //任务管理面板
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = false;
        }
    }
}