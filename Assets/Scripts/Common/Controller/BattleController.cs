using System.Collections.Generic;
using UnityEngine;


//  상태 기반으로 전투 상황 관리로 StateMachine을 이용
public class BattleController : StateMachine
{
    #region Public
    //  카메라
    public CameraRig cameraRig;
    //  맵 정보
    public Board board;
    public LevelData levelData;
    
    // 선택 타일 표시
    public Transform tileSelectionIndicator;
    //  선택 타일 정보
    public Point pos;
    
    //  맵에 영웅을 인스턴트화
    public GameObject heroPrefab;

    public Tile currentTile { get { return board.GetTile(pos); } }

    // 능력 선택 메뉴 UI
    public AbilityMenuPanelController abilityMenuPanelController;
    //  턴 정보
    public Turn turn = new Turn();
    //  맵에 전투중인 유닛들
    public List<Unit> units = new List<Unit>();
    //  전투 스텟 UI 참조용
    public StatPanelController statPanelController;

    #endregion

    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}