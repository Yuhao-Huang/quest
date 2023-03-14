using System;
using UnityEngine;

/// <summary>
/// 完成任务的媒介，如一个地点放这个脚本
/// </summary>
public class CollisionHandler:MonoBehaviour
{
    public string sceneToLoad;
    public string spawnPointName;

    private void OnTriggerEnter(Collider other)
    {
        //填充任务需求
        QuestManager.questManager.AddQuestItem("Leave Town 1",1);
    }
}