using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //  ��ġ�� ���� ����
    public Point pos;
    public int height;
    //  ���� ���� 4�ܰ�
    public const float stepHeight = 0.25f;
    
    //  Ÿ�� �߽ɿ� ĳ���Ͱ� ��ġ�ϵ���
    public Vector3 centor { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }
   
    //  Ÿ�� ����
    public void Grow()
    {
        height++;
        Match();
    }

    public void Shrink()
    {
        height--;
        Match();
    }

    //  Ÿ�� ���� ��������
    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        Match();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

    // �ð��� ������Ʈ�� ���� �Լ�
    private void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }
}
