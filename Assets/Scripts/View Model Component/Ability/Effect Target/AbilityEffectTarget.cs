using UnityEngine;
using System.Collections;

//  대상에 따라 효과가 다른 경우 고려
public abstract class AbilityEffectTarget : MonoBehaviour
{
    public abstract bool IsTarget(Tile tile);
}