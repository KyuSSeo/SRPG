using UnityEngine;
using System.Collections;

//  Ability 를 트리처럼 관리하는 카탈로그
public class AbilityCatalog : MonoBehaviour
{

    //  자식 반환
    public int CategoryCount()
    {
        return transform.childCount;
    }

    // 인덱스의 GameObject 반환
    public GameObject GetCategory(int index)
    {
        if (index < 0 || index >= transform.childCount)
            return null;
        return transform.GetChild(index).gameObject;
    }

    // 카테고리 내의 스킬 개수 반환
    public int AbilityCount(GameObject category)
    {
        return category != null ? category.transform.childCount : 0;
    }

    //  Ability 반환
    public Ability GetAbility(int categoryIndex, int abilityIndex)
    {
        GameObject category = GetCategory(categoryIndex);
        //  유효성
        if (category == null || abilityIndex < 0 || abilityIndex >= category.transform.childCount)
            return null;
        //  컴포넌트를 반환
        return category.transform.GetChild(abilityIndex).GetComponent<Ability>();
    }
}