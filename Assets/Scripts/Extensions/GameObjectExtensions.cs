using UnityEngine;
using System.Collections;

public static class GameObjectExtensions
{
    //  자식 컴포넌트 추가시키기
    public static T AddChildComponent<T>(this GameObject obj) where T : MonoBehaviour
    {
        GameObject child = new GameObject(typeof(T).Name);
        child.transform.SetParent(obj.transform);
        return child.AddComponent<T>();
    }
}