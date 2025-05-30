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


    void Start()
    {
        ChangeState<InitBattleState>();
    }
}