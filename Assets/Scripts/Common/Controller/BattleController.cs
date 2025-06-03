using System.Collections.Generic;
using UnityEngine;


//  ���� ������� ���� ��Ȳ ������ StateMachine�� �̿�
public class BattleController : StateMachine
{
    #region Public
    //  ī�޶�
    public CameraRig cameraRig;
    //  �� ����
    public Board board;
    public LevelData levelData;
    
    // ���� Ÿ�� ǥ��
    public Transform tileSelectionIndicator;
    //  ���� Ÿ�� ����
    public Point pos;
    
    //  �ʿ� ������ �ν���Ʈȭ
    public GameObject heroPrefab;

    public Tile currentTile { get { return board.GetTile(pos); } }

    // �ɷ� ���� �޴� UI
    public AbilityMenuPanelController abilityMenuPanelController;
    //  �� ����
    public Turn turn = new Turn();
    //  �ʿ� �������� ���ֵ�
    public List<Unit> units = new List<Unit>();
    //  ���� ���� UI ������
    public StatPanelController statPanelController;

    #endregion

    private void Start()
    {
        ChangeState<InitBattleState>();
    }
}