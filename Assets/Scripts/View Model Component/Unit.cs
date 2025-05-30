using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  ���忡�� �ش� ������ ��� Ÿ�Ͽ� ��ġ�Ǿ� �ִ���
//  � ������ ���� �ִ��� Ȯ���ϴ� ��ũ��Ʈ
public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;


    //  ������ Ÿ�� ���� ��ġ, Ÿ�ϰ��� ���� ����, �缳��
    public void Place(Tile target)
    {
        // ���� Ÿ���� �� ������ content�� ������ �ִٸ� ���� ����
        if (tile != null && tile.contents == gameObject)
            tile.contents = null;

        // �� Ÿ�Ϸ� ������ ������ ����
        tile = target;
        // �� Ÿ���� �����ϸ�, �ش� Ÿ���� content�� �� ���� ���
        if (target != null)
            target.contents = gameObject;
    }

    //  Ÿ�� �߾ӿ� ��ġ, ������ �ٶ󺸵��� ����
    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}
