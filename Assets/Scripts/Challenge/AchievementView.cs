using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;  // 업적 슬롯 프리팹
    [SerializeField] private Transform contentParent;
    private Dictionary<int, AchievementSlot> achievementSlots = new();

    public void CreateAchievementSlots(AchievementSO[] achievements)
    {      
        // achievement 데이터에 따라 슬롯을 생성함
        foreach (AchievementSO data in achievements)
        {
            GameObject slotObj = Instantiate(achievementSlotPrefab, contentParent);
            AchievementSlot slot = slotObj.GetComponent<AchievementSlot>();
            slot.Init(data);
            //slot.transform.localScale = Vector3.one;
            achievementSlots.Add(data.threshold, slot);
        }
    }

    public void UnlockAchievement(int threshold)
    {
        // UI 반영 로직
        achievementSlots[threshold].MarkAsChecked();
    }
}