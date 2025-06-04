using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  ���õ� �ൿ�� �ϴ� ����
public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    //  ���� �� �ൿ ����Ʈ īŻ�α׿��� �޾ƿ���
    private AbilityCatalog catalog;

    // �ൿ ��� �ε� �� �޴� ǥ��
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
            //  īŻ�α׿��� ��� ��������
            Ability ability = catalog.GetAbility(category, i);
            AbilityMagicCost cost = ability.GetComponent<AbilityMagicCost>();
            if (cost)
                menuOptions.Add(string.Format("{0}: {1}", ability.name, cost.amount));
            else
                menuOptions.Add(ability.name);
            //  ��ݼ���
            locks[i] = !ability.CanPerform();
        }

        // �޴� UI ǥ��
        abilityMenuPanelController.Show(menuTitle, menuOptions);

        // ��� ����
        for (int i = 0; i < count; ++i)
            abilityMenuPanelController.SetLocked(i, locks[i]);
    }

    // ���� �ൿ ó�� �Ϸ�
    protected override void Confirm()
    {
        turn.ability = catalog.GetAbility(category, abilityMenuPanelController.selection);
        owner.ChangeState<AbilityTargetState>();
    }

    //  �ൿ ������ ���� ȭ������ ���ư���
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }
}