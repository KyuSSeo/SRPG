using System.Collections.Generic;


//  행동에서 어떠한 행동을 할 것인지 고르기
public class CategorySelectionState : BaseAbilityMenuState
{
    public override void Enter()
    {
        base.Enter();
        statPanelController.ShowPrimary(turn.actor.gameObject);
    }

    public override void Exit()
    {
        base.Exit();
        statPanelController.HidePrimary();
    }

    //  행동 하위 매뉴
    protected override void LoadMenu()
    {
        if (menuOptions == null)
            menuOptions = new List<string>();
        else
            menuOptions.Clear();


        //  트리에서 Action 만 별개의 항목으로 관리
        menuTitle = "Action";
        menuOptions.Add("Attack");
        //  나머지 항목 출력
        AbilityCatalog catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        for (int i = 0; i < catalog.CategoryCount(); ++i)
            menuOptions.Add(catalog.GetCategory(i).name);

        abilityMenuPanelController.Show(menuTitle, menuOptions);

    }
    // 메뉴 표시


    //  행동 하위 매뉴 중 선택했을 때
    protected override void Confirm()
    {
        if (abilityMenuPanelController.selection == 0)
            Attack();
        else
            SetCategory(abilityMenuPanelController.selection - 1);
    }

    //  이전 상태로 돌아가기
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    // 기본 공격 수행
    private void Attack()
    {
        //  이동했을 경우 위치 고정
        turn.ability = turn.actor.GetComponentInChildren<Ability>();
        owner.ChangeState<AbilityTargetState>();
    }

    //  공격 외 행동 선택하는 리스트상태
    private void SetCategory(int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}