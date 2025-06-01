using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  ���õ� �ൿ�� �ϴ� ����
public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    //  ���� �� �ൿ ����Ʈ
    private string[] whiteMagicOptions = new string[] { "Cure", "Raise", "Holy" };
    private string[] blackMagicOptions = new string[] { "Fire", "Ice", "Lightning" };

    // �ൿ ��� �ε� �� �޴� ǥ��
    protected override void LoadMenu()
    {
        if (menuOptions == null)
            menuOptions = new List<string>(3);
        if (category == 0)
        {
            menuTitle = "White Magic";
            SetOptions(whiteMagicOptions);
        }
        else
        {
            menuTitle = "Black Magic";
            SetOptions(blackMagicOptions);
        }
        // �޴� UI ǥ��
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }

    // ���� �ൿ ó�� �Ϸ�
    protected override void Confirm()
    {
        // �̹� �̵������� �̵� ����
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        //  �ٽ� ��� �޴���
        owner.ChangeState<CommandSelectionState>();
    }

    //  �ൿ ������ ���� ȭ������ ���ư���
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }

    // ���� ������ �ൿ �޴� ����
    private void SetOptions(string[] options)
    {
        menuOptions.Clear();
        for (int i = 0; i < options.Length; ++i)
            menuOptions.Add(options[i]);
    }
}