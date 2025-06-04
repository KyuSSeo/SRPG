using System.Collections.Generic;


//  �ൿ���� ��� �ൿ�� �� ������ ����
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

    //  �ൿ ���� �Ŵ�
    protected override void LoadMenu()
    {
        if (menuOptions == null)
            menuOptions = new List<string>();
        else
            menuOptions.Clear();


        //  Ʈ������ Action �� ������ �׸����� ����
        menuTitle = "Action";
        menuOptions.Add("Attack");
        //  ������ �׸� ���
        AbilityCatalog catalog = turn.actor.GetComponentInChildren<AbilityCatalog>();
        for (int i = 0; i < catalog.CategoryCount(); ++i)
            menuOptions.Add(catalog.GetCategory(i).name);

        abilityMenuPanelController.Show(menuTitle, menuOptions);

    }
    // �޴� ǥ��


    //  �ൿ ���� �Ŵ� �� �������� ��
    protected override void Confirm()
    {
        if (abilityMenuPanelController.selection == 0)
            Attack();
        else
            SetCategory(abilityMenuPanelController.selection - 1);
    }

    //  ���� ���·� ���ư���
    protected override void Cancel()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    // �⺻ ���� ����
    private void Attack()
    {
        //  �̵����� ��� ��ġ ����
        turn.ability = turn.actor.GetComponentInChildren<Ability>();
        owner.ChangeState<AbilityTargetState>();
    }

    //  ���� �� �ൿ �����ϴ� ����Ʈ����
    private void SetCategory(int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}