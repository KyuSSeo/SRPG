using System.Collections;
using System.Collections.Generic;

//  ��ų Ÿ���� ����
public class AbilityTargetState : BattleState
{
    List<Tile> tiles;
    AbilityRange ar;


    //  ���� ����, Ż��
    public override void Enter()
    {
        base.Enter();
        ar = turn.ability.GetComponent<AbilityRange>();
        SelectTiles();
        statPanelController.ShowPrimary(turn.actor.gameObject);
        if (ar.directionOriented)
            RefreshSecondaryStatPanel(pos);
    }
    public override void Exit()
    {
        base.Exit();
        board.DeSelectTiles(tiles);
        statPanelController.HidePrimary();
        statPanelController.HideSecondary();
    }
    // ���� �Է¿� ���� ������
    protected override void OnMove(object sender, InfoEventArgs<Point> e)
    {
        if (ar.directionOriented)
        {
            ChangeDirection(e.info);
        }
        else
        {
            SelectTile(e.info + pos);
            RefreshSecondaryStatPanel(pos);
        }
    }
    /// Ȯ��/��� ó��
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        //  ����� ��ȿ�� ���� �Ǵ�
        if (e.info == 0)
        {
            if (ar.directionOriented || tiles.Contains(board.GetTile(pos)))
                owner.ChangeState<ConfirmAbilityTargetState>();
        }
        else
        {
            owner.ChangeState<CategorySelectionState>();
        }
    }
    //  ���� ����
    private void ChangeDirection(Point p)
    {
        Directions dir = p.GetDirection();
        if (turn.actor.dir != dir)
        {
            board.DeSelectTiles(tiles);
            turn.actor.dir = dir;
            turn.actor.Match();
            SelectTiles();
        }
    }
    //  Ÿ���� �����ϴ� ���
    private void SelectTiles()
    {
        tiles = ar.GetTilesInRange(board);
        board.SelectTiles(tiles);
    }
}