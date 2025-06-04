using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  선택된 행동을 하는 상태
public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    //  상태 별 행동 리스트 카탈로그에서 받아오기
    private AbilityCatalog catalog;

    // 행동 목록 로드 및 메뉴 표시
    protected override void LoadMenu()
    {
        catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        GameObject container = catalog.GetCategory(category);
        menuTitle = container.name;

        int count = catalog.AbilityCount(container);
        if (menuOptions == null)
            menuOptions = new List<string>(count);
        else
            menuOptions.Clear();

        bool[] locks = new bool[count];

        for (int i = 0; i < count; ++i)
        {
            //  카탈로그에서 목록 가져오기
            Ability ability = catalog.GetAbility(category, i);
            AbilityMagicCost cost = ability.GetComponent<AbilityMagicCost>();
            if (cost)
                menuOptions.Add(string.Format("{0}: {1}", ability.name, cost.amount));
            else
                menuOptions.Add(ability.name);
            //  잠금설정
            locks[i] = !ability.CanPerform();
        }

        // 메뉴 UI 표시
        abilityMenuPanelController.Show(menuTitle, menuOptions);

        // 잠금 설정
        for (int i = 0; i < count; ++i)
            abilityMenuPanelController.SetLocked(i, locks[i]);
    }

    // 선택 행동 처리 완료
    protected override void Confirm()
    {
        turn.ability = catalog.GetAbility(category, abilityMenuPanelController.selection);
        owner.ChangeState<AbilityTargetState>();
    }

    //  행동 이전의 선택 화면으로 돌아가기
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }
}