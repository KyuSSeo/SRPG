using UnityEngine;
using System.Collections;

//  ��� ���� ȿ���� �ٸ� ��� ���
public abstract class AbilityEffectTarget : MonoBehaviour
{
    public abstract bool IsTarget(Tile tile);
}