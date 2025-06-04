using UnityEngine;
using System.Collections;

//  Ability �� Ʈ��ó�� �����ϴ� īŻ�α�
public class AbilityCatalog : MonoBehaviour
{

    //  �ڽ� ��ȯ
    public int CategoryCount()
    {
        return transform.childCount;
    }

    // �ε����� GameObject ��ȯ
    public GameObject GetCategory(int index)
    {
        if (index < 0 || index >= transform.childCount)
            return null;
        return transform.GetChild(index).gameObject;
    }

    // ī�װ� ���� ��ų ���� ��ȯ
    public int AbilityCount(GameObject category)
    {
        return category != null ? category.transform.childCount : 0;
    }

    //  Ability ��ȯ
    public Ability GetAbility(int categoryIndex, int abilityIndex)
    {
        GameObject category = GetCategory(categoryIndex);
        //  ��ȿ��
        if (category == null || abilityIndex < 0 || abilityIndex >= category.transform.childCount)
            return null;
        //  ������Ʈ�� ��ȯ
        return category.transform.GetChild(abilityIndex).GetComponent<Ability>();
    }
}