using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleState : State
{
    protected BattleController owner;

    //  BattleController 참조 가져오기
    public CameraRig cameraRig { get { return owner.cameraRig; } }
    public Board board { get { return owner.board; } }
    public LevelData levelData { get { return owner.levelData; } }
    public Transform tileSelectionIndicator { get { return owner.tileSelectionIndicator; } }
    public Point pos { get { return owner.pos; } set { owner.pos = value; } }
    public Turn turn { get { return owner.turn; } }
    public List<Unit> units { get { return owner.units; } }
    public Tile currentTile { get { return owner.currentTile; } }
    public StatPanelController statPanelController { get { return owner.statPanelController; } }
    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; } }

    //  초기화
    protected virtual void Awake() => Init();
    private void Init()
    {
        owner = GetComponent<BattleController>();
    }

    // 상태가 활성화될 때 상태 설정
    protected virtual void Start()
    {
        owner.ChangeState<CutSceneState>();
    }

    //  이벤트 등록

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

    //  가상함수로 구체화 해야함

    protected virtual void OnMove(object sender, InfoEventArgs<Point> e)
    {

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e)
    {

    }

    //  지정된 위치에 타일이 있을 때, 타일을 설정
    protected virtual void SelectTile(Point p)
    {
        if (pos == p || !board.tiles.ContainsKey(p))
            return;
        pos = p;
        tileSelectionIndicator.localPosition = board.tiles[p].center;
    }

    protected virtual Unit GetUnit(Point p)
    {
        Tile t = board.GetTile(p);
        GameObject content = t != null ? t.content : null;
        return content != null ? content.GetComponent<Unit>() : null;
    }
    protected virtual void RefreshPrimaryStatPanel(Point p)
    {
        Unit target = GetUnit(p);
        if (target != null)
            statPanelController.ShowPrimary(target.gameObject);
        else
            statPanelController.HidePrimary();
    }
    protected virtual void RefreshSecondaryStatPanel(Point p)
    {
        Unit target = GetUnit(p);
        if (target != null)
            statPanelController.ShowSecondary(target.gameObject);
        else
            statPanelController.HideSecondary();
    }
}
