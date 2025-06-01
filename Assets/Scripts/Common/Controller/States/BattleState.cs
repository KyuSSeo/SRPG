using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleState : State
{
    protected BattleController owner;

    //  BattleController ���� ��������
    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public Board board { get { return owner.board; } }
    public LevelData levelData { get { return owner.levelData; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }
    public Turn turn { get { return owner.turn; } }
    public List<Unit> units { get { return owner.units; } }

    //  �ʱ�ȭ
    protected virtual void Awake() => Init();
    private void Init()
    {
        owner = GetComponent<BattleController>();
    }

    // ���°� Ȱ��ȭ�� �� ���� ����
    protected virtual void Start()
    {
        owner.ChangeState<CutSceneState>();
    }

    //  �̺�Ʈ ���

    protected override void AddListeners()
    {
        InputController.moveEvent += OnMove;
        InputController.fireEvent += OnFire;
    }
    protected override void RemoveListeners()
    {
        InputController.moveEvent -= OnMove;
        InputController.fireEvent -= OnFire;
    }

    //  �����Լ��� ��üȭ �ؾ���

    protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    {

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e)
    {

    }

    //  ������ ��ġ�� Ÿ���� ���� ��, Ÿ���� ����
    protected virtual void SelectTile(Point p)
    {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;
        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }
}
