using System.Collections.Generic;
using UnityEngine;


//  ���� ������� ���� ��Ȳ ������ StateMachine�� �̿�
public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    
    //  �ʿ� ������ �ν���Ʈȭ
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