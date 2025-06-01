using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 능력 메뉴 상태
public abstract class BaseAbilityMenuState : BattleState
{
    //  메뉴 항목들
    protected string menuTitle;
    protected List<string> menuOptions;

    public override void Enter()
    {
        //  진입 시 메뉴 불러오기
        base.Enter();
        SelectTile(turn.actor.tile.pos);
        LoadMenu();
    }

    public override void Exit()
    {
        //  상태 탈출 시 메뉴 숨기기
        base.Exit();
        abilityMenuPanelController.Hide();
    }

    // 확인/취소 입력 처리
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        if (e.info == 0)
            //  확인
            Confirm();
        else
            //  취소, 대기
            Cancel();
    }

    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (e.info.x > 0 || e.info.y < 0)
            abilityMenuPanelController.Next();
        else
            abilityMenuPanelController.Previous();
    }

    //  메뉴 로드
    protected abstract void LoadMenu();
    //  메뉴 확인, 선택
    protected abstract void Confirm();
    //  메뉴 취소, 대기
    protected abstract void Cancel();
}
