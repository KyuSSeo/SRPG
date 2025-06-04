using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  �� ȹ��
public class TurnOrderController : MonoBehaviour
{
    //  �� ȹ�� ī����
    private const int turnActivation = 1000;
    //  �Ҹ� �ൿ �ڽ�Ʈ
    private const int turnCost = 500;
    private const int moveCost = 300;
    private const int actionCost = 200;

    //  �˸�
    public const string RoundBeganNotification = "TurnOrderController.roundBegan";              // ���� ����
    public const string RoundEndedNotification = "TurnOrderController.roundEnded";              // ���� ����
    public const string TurnCheckNotification = "TurnOrderController.turnCheck";                // �� üũ
    public const string TurnCompletedNotification = "TurnOrderController.turnCompleted";        // �� �Ϸ�
    public const string TurnBeganNotification = "TurnOrderController.TurnBeganNotification";    // �� ����

    //  CRT �� ���� �� ȹ�� �ڷ�ƾ
    public IEnumerator Round()
    {
        BattleController bc = GetComponent<BattleController>(); ;
        while (true)
        {
            //  ����
            this.PostNotification(RoundBeganNotification);
            //  ���� ����
            List<Unit> units = new List<Unit>(bc.units);

            //  ���ǵ�� �� ���
            for (int i = 0; i < units.Count; ++i)
            {
                Stats s = units[i].GetComponent<Stats>();
                s[StatTypes.CTR] += s[StatTypes.SPD];
            }
            //  �� ������ �������� ����
            units.Sort((a, b) => GetCounter(a).CompareTo(GetCounter(b)));

            //  ���� �ݺ���
            for (int i = units.Count - 1; i >= 0; --i)
            {
                //  ī���͸� ������ ��쿡��
                if (CanTakeTurn(units[i]))
                {
                    bc.turn.Change(units[i]);
                    //  �� ���� �˸�
                    units[i].PostNotification(TurnBeganNotification);
                    //  �� ����
                    yield return units[i];

                    // �ൿ ��� ���
                    int cost = turnCost;
                    if (bc.turn.hasUnitMoved)
                        cost += moveCost;
                    if (bc.turn.hasUnitActed)
                        cost += actionCost;

                    //  ��븸ŭ ����
                    Stats s = units[i].GetComponent<Stats>();
                    s.SetValue(StatTypes.CTR, s[StatTypes.CTR] - cost, false);
                    
                    // �ش� ������ �� �Ϸ� �˸�
                    units[i].PostNotification(TurnCompletedNotification);
                }
            }

            // ���� ���� �˸�
            this.PostNotification(RoundEndedNotification);
        }
    }
    // ���� ȹ�� ����
    private bool CanTakeTurn(Unit target)
    {
        BaseException exc = new BaseException(GetCounter(target) >= turnActivation);
        target.PostNotification(TurnCheckNotification, exc);
        return exc.toggle;
    }
    // ������ ���� CTR ��ȯ
    private int GetCounter(Unit target)
    {
        return target.GetComponent<Stats>()[StatTypes.CTR];
    }
}