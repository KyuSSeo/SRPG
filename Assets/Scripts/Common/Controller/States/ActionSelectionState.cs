using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//  선택된 행동을 하는 상태
public class ActionSelectionState : BaseAbilityMenuState
{
    public static int category;
    //  상태 별 행동 리스트
    private string[] whiteMagicOptions = new string[] { "Cure", "Raise", "Holy" };
    private string[] blackMagicOptions = new string[] { "Fire", "Ice", "Lightning" };

    // 행동 목록 로드 및 메뉴 표시
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
        // 메뉴 UI 표시
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }

    // 선택 행동 처리 완료
    protected override void Confirm()
    {
        // 이미 이동했으면 이동 고정
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
            turn.lockMove = true;
        //  다시 명령 메뉴로
        owner.ChangeState<CommandSelectionState>();
    }

    //  행동 이전의 선택 화면으로 돌아가기
    protected override void Cancel()
    {
        owner.ChangeState<CategorySelectionState>();
    }

    // 선택 가능한 행동 메뉴 설정
    private void SetOptions(string[] options)
    {
        menuOptions.Clear();
        for (int i = 0; i < options.Length; ++i)
            menuOptions.Add(options[i]);
    }
}