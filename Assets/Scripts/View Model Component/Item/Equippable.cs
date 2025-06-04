using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippable : MonoBehaviour
{
    //  장비 필드
    public EquipSlots defaultSlots;
    public EquipSlots secondarySlots;
    public EquipSlots slots;
    //  장비 여부
    private bool _isEquipped;


    //  장비 시
    public void OnEquip()
    {
        if (_isEquipped)
            return;
        _isEquipped = true;
        Features[] features = GetComponentsInChildren<Features>();
        for (int i = 0; i < features.Length; ++i)
            features[i].Activate(gameObject);
    }

    //  장비 해제 시
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
