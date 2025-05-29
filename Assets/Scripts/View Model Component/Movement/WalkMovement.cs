using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� ����
public class WalkMovement : Movement
{
    //  Ÿ�� ���̸� ���Ͽ� ������ �����Ѱ�?
    protected override bool ExpandSearch(Tile from, Tile tile)
    {
        //  ���� ���̺��� Ÿ���� ������
        if ((Mathf.Abs(from.height - tile.height) > jumpHeight))
            return false;
        //  ������ ���������� ���𰡰� Ÿ���� �������̸�
        if (tile.contents != null)
            return false;
        return base.ExpandSearch(from, tile);
    }
    public override IEnumerator Traverse(Tile tile)
    {
        unit.Place(tile);

        //  ���������� ���������� ��� �ۼ�
        List<Tile> targets = new List<Tile>();
        while (tile != null)
        {
            targets.Insert(0, tile);
            tile = tile.prev;
        }
        //  ��θ� ���������� �̵�
        for (int i = 0; i < targets.Count; i++)
        {
            //  ���� ��ġ���� ���� ��ġ��
            Tile from = targets[i - 1];
            Tile to = targets[i];
            //  ���� ��������
            Directions dir = from.GetDirections(to);

            if (unit.dir != dir)
                yield return StartCoroutine(Walk(to));
            if (from.height == to.height)
                yield return StartCoroutine(Jump(to));
        }
        yield return null;
    }

    //  �̵�
    private IEnumerator Walk(Tile target)
    {
        //  Tweener : ������Ʈ�� ��ġ, ȸ��, ����, ũ�� ���� �ε巴�� ��ȭ��Ű�� ���� ������ ���, �ַ� �ִϸ��̼� ó��
        //  EasingEquations : Tweener Ŭ���� ���� ��Ʈ�ѷ�, �ð� ����, ���� ���¸� ����
        #region Lerp�� �������� ����?
        /*
        Lerp �� ���� ���� ����
        Vector3 start = transform.position;
        Vector3 end = new Vector3(5, 0, 0);
        float t = 0;

        void Update()
        {
            if (t < 1f)
            {
                t += Time.deltaTime;
                transform.position = Vector3.Lerp(start, end, t);
            }
        }

        Tweener �ڵ� ����
        transform.MoveTo(new Vector3(5, 0, 0), 1f, EasingEquations.EaseOutBounce);

        */
        #endregion

        Tweener tweener = transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        while (tweener != null)
            yield return null;
    }
    //  ����
    private IEnumerator Jump(Tile target)
    {
        Tweener tweener = transform.MoveTo(target.center, 0.5f, EasingEquations.Linear);
        Tweener tweener2 = jumper.MoveToLocal(
            new Vector3(0, Tile.stepHeight * 2f, 0),
            tweener.easingControl.duration / 2f,
            EasingEquations.EaseOutQuad
        );

        while (tweener != null)
            yield return null;
    }
}
