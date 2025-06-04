using UnityEngine;
using System.Collections;

public class DurationStatusCondition : StatusCondition
{
    // 상태 유지 턴 
    public int duration = 10;

    //  매 라운드 시작마다 호출
    private void OnEnable()
    {
        this.AddObserver(OnNewTurn, TurnOrderController.RoundBeganNotification);
    }

    private void OnDisable()
    {
        this.RemoveObserver(OnNewTurn, TurnOrderController.RoundBeganNotification);
    }
    //  턴 종료시 상태 해제 여부
    private void OnNewTurn(object sender, object args)
    {
        duration--;
        if (duration <= 0)
            Remove();
    }
}