using System.Collections.Generic;
using UnityEngine;


//  상태 기반으로 전투 상황 관리로 StateMachine을 이용
public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    
    //  맵에 영웅을 인스턴트화
    public GameObject heroPrefab;
    public Unit currentUnit;
    public Tile currentTile { get { return board.GetTile(pos); } }

    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();


    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}