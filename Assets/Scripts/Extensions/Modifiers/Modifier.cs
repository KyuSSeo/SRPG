using UnityEngine;
using System.Collections;

//  �׳� TPS �Ұ�, ���ǿ��� �������ڵ尡 20�ܰ迡 ���ļ� �̷�������.
public abstract class Modifier
{
    //  ��� �����ڴ� ���� ������ ������
    public readonly int sortOrder;
    public Modifier(int sortOrder)
    {
        this.sortOrder = sortOrder;
    }
}
