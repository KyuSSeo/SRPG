using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  턴 획득
public class TurnOrderController : MonoBehaviour
{
    //  턴 획득 카운터
    private const int turnActivation = 1000;
    //  소모 행동 코스트
    private const int turnCost = 500;
    private const int moveCost = 300;
    private const int actionCost = 200;

    //  알림
    public const string RoundBeganNotification = "TurnOrderController.roundBegan";              // 라운드 시작
    public const string RoundEndedNotification = "TurnOrderController.roundEnded";              // 라운드 종료
    public const string TurnCheckNotification = "TurnOrderController.turnCheck";                // 턴 체크
    public const string TurnCompletedNotification = "TurnOrderController.turnCompleted";        // 턴 완료
    public const string TurnBeganNotification = "TurnOrderController.TurnBeganNotification";    // 턴 시작

    //  CRT 에 따라서 턴 획득 코루틴
    public IEnumerator Round()
    {
        BattleController bc = GetComponent<BattleController>(); ;
        while (true)
        {
            //  시작
            this.PostNotification(RoundBeganNotification);
            //  전투 유닛
            List<Unit> units = new List<Unit>(bc.units);

            //  스피드로 턴 계산
            for (int i = 0; i < units.Count; ++i)
            {
                Stats s = units[i].GetComponent<Stats>();
                s[StatTypes.CTR] += s[StatTypes.SPD];
            }
            //  턴 순서로 오름차순 정렬
            units.Sort((a, b) => GetCounter(a).CompareTo(GetCounter(b)));

            //  역순 반복문
            for (int i = units.Count - 1; i >= 0; --i)
            {
                //  카운터를 만족한 경우에만
                if (CanTakeTurn(units[i]))
                {
                    bc.turn.Change(units[i]);
                    //  턴 시작 알림
                    units[i].PostNotification(TurnBeganNotification);
                    //  턴 실행
                    yield return units[i];

                    // 행동 비용 계산
                    int cost = turnCost;
                    if (bc.turn.hasUnitMoved)
                        cost += moveCost;
                    if (bc.turn.hasUnitActed)
                        cost += actionCost;

                    //  비용만큼 차감
                    Stats s = units[i].GetComponent<Stats>();
                    s.SetValue(StatTypes.CTR, s[StatTypes.CTR] - cost, false);
                    
                    // 해당 유닛의 턴 완료 알림
                    units[i].PostNotification(TurnCompletedNotification);
                }
            }

            // 라운드 종료 알림
            this.PostNotification(RoundEndedNotification);
        }
    }
    // 차례 획득 여부
    private bool CanTakeTurn(Unit target)
    {
        BaseException exc = new BaseException(GetCounter(target) >= turnActivation);
        target.PostNotification(TurnCheckNotification, exc);
        return exc.toggle;
    }
    // 유닛의 현재 CTR 반환
    private int GetCounter(Unit target)
    {
        return target.GetComponent<Stats>()[StatTypes.CTR];
    }
}