using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : MonoBehaviour
{
    //  ��� �ʵ�
    public EquipSlots defaultSlots;
    public EquipSlots secondarySlots;
    public EquipSlots slots;
    //  ��� ����
    private bool _isEquipped;


    //  ��� ��
    public void OnEquip()
    {
        if (_isEquipped)
            return;
        _isEquipped = true;
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Activate(gameObject);
    }

    //  ��� ���� ��
    public void OnUnEquip()
    {
        if (!_isEquipped)
            return;
        _isEquipped = false;
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Deactivate();
    }
}
