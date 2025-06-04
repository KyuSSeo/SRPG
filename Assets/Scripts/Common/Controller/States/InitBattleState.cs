using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전투 시작 시 초기 설정 및 타일을 처리
public class InitBattleState :BattleState
{

    // 진입 시 기본 처리 후 코루틴 동작
    public override void Enter()
    {
        base.Enter();
        StartCoroutine(Init());
    }

    // 초기화 코루틴: 맵 로드, 초기 타일 선택, 다음 상태 전환을 순차적으로 수행
    // 맵 생성이 끝날 때 까지 기다뎌야 함.
    private IEnumerator Init()
    {   
        //  타일 생성
        board.Load(levelData);
        Point p = new Point((int)levelData.tiles[0].x, (int)levelData.tiles[0].z);
        //  초기 타일 선택
        SelectTile(p);
        SpawnTestUnits();   // TODO : 임시 코드.
        //  라운드 정보 추가
        owner.round = owner.gameObject.AddComponent<TurnOrderController>().Round();
        yield return null;
        //  맵 초기화 완료 후 상태 전환
        owner.ChangeState<CutSceneState>();
    }


    //  유닛 배치 테스트 함수
    private void SpawnTestUnits()
    {

        //  유닛 타입
        string[] jobs = new string[] { "Rogue", "Warrior", "Wizard" };
        
        //  유닛 생성
        for (int i = 0; i < jobs.Length; ++i)
        {
            // 미리 설정된 프리팹을 인스턴스화
            GameObject instance = Instantiate(owner.heroPrefab) as GameObject;
            
            //  초기 레벨 설정
            Stats s = instance.AddComponent<Stats>();
            s[StatTypes.LVL] = 1;

            //  직업 등록
            GameObject jobPrefab = Resources.Load<GameObject>("Jobs/" + jobs[i]);
            GameObject jobInstance = Instantiate(jobPrefab) as GameObject;
            jobInstance.transform.SetParent(instance.transform);
            Job job = jobInstance.GetComponent<Job>();

            //  직업 적용, 능력치 적용
            job.Employ();
            job.LoadDefaultStats();

            // 해당 유닛을 배치할 좌표 설정
            Point p = new Point((int)levelData.tiles[i].x, (int)levelData.tiles[i].z);

            //  유닛 컴포넌트를 가져와서 배치
            Unit unit = instance.GetComponent<Unit>();
            unit.Place(board.GetTile(p));
            unit.Match();

            //  이동요소 추가
            instance.AddComponent<WalkMovement>();

            //  전투 리스트에 유닛 추가
            units.Add(unit);
            Rank rank = instance.AddComponent<Rank>();
            rank.Init(10);

            // Hp, Mp 정보 추가
            instance.AddComponent<Health>();
            instance.AddComponent<Mana>();

            instance.name = jobs[i];
        }
    }
}
