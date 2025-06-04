using UnityEngine;
using System.Collections;

//  비트 연산을 통하여 여러 장비 확인
[System.Flags]
public enum EquipSlots
{
    None = 0,
    Primary = 1 << 0,   // 손 1
    Secondary = 1 << 1, // 손 2
    Head = 1 << 2,      // 머리
    Body = 1 << 3,      // 몸통
    Accessory = 1 << 4	// 장신구
}