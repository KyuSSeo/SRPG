using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� �� �ʱ� ���� �� Ÿ���� ó��
public class InitBattleState :BattleState
{

    // ���� �� �⺻ ó�� �� �ڷ�ƾ ����
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    // �ʱ�ȭ �ڷ�ƾ: �� �ε�, �ʱ� Ÿ�� ����, ���� ���� ��ȯ�� ���������� ����
    // �� ������ ���� �� ���� ��ٵ��� ��.
    private IEnumerator Init()
    {   
        //  Ÿ�� ����
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        //  �ʱ� Ÿ�� ����
        SelectTile(p);
        SpawnTestUnits();   // TODO : �ӽ� �ڵ�.
        //  ���� ���� �߰�
        owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
        yield return null;
        //  �� �ʱ�ȭ �Ϸ� �� ���� ��ȯ
        owner.ChangeState<CutSceneState>();
    }


    //  ���� ��ġ �׽�Ʈ �Լ�
    private void SpawnTestUnits()
    {

        //  ���� Ÿ��
        string[] jobs = new string[] { "Rogue", "Warrior", "Wizard" };
        
        //  ���� ����
        for (int i = 0; i < jobs.Length; ++i)
        {
            // �̸� ������ �������� �ν��Ͻ�ȭ
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            
            //  �ʱ� ���� ����
            Stats s = instance.AddComponent<Stats>();
            s[StatTypes.LVL] = 1;

            //  ���� ���
            GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
            GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
            jobInstance.transform.SetParent(instance.transform);
            Job job = jobInstance.GetComponent<Job>();

            //  ���� ����, �ɷ�ġ ����
            job.Employ();
            job.LoadDefaultStats();

            // �ش� ������ ��ġ�� ��ǥ ����
            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);

            //  ���� ������Ʈ�� �����ͼ� ��ġ
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            //  �̵���� �߰�
            instance.AddComponent<WalkMovement>();

            //  ���� ����Ʈ�� ���� �߰�
            units.Add(unit);
            Rank rank = instance.AddComponent<Rank>();
            rank.Init(10);

            // Hp, Mp ���� �߰�
            instance.AddComponent<Health>();
            instance.AddComponent<Mana>();

            instance.name = jobs[i];
        }
    }
}
