using UnityEngine;
using System.Collections;

//  ��Ʈ ������ ���Ͽ� ���� ��� Ȯ��
[System.Flags]
public enum EquipSlots
{
    None = 0,
    Primary = 1 << 0,   // �� 1
    Secondary = 1 << 1, // �� 2
    Head = 1 << 2,      // �Ӹ�
    Body = 1 << 3,      // ����
    Accessory = 1 << 4	// ��ű�
}