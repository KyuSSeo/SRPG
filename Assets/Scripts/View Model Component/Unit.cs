using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ���忡�� �ش� ������ ��� Ÿ�Ͽ� ��ġ�Ǿ� �ִ���
//  � ������ ���� �ִ��� Ȯ���ϴ� ��ũ��Ʈ
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;

    public void Place(Tile target)
    {
        if (tile != null && tile.contents == gameObject)
            tile.contents = null;
        tile = target;

        if (target != null)
            target.contents = gameObject;
    }
    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}
