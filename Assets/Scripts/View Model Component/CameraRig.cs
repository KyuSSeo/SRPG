using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//  보드 위에서 타일을 선택 시 카메라가 따라가는 기능
public class CameraRig : MonoBehaviour
{   
    public float speed = 3f;
    public Transform follow;
    private Transform _transform;

    private void Awake() => Init();
    private void Init()
    {
        _transform = transform;
    }
    private void Update()
    {
        if (follow)
        {
            transform.position = Vector3.Lerp(_transform.position, follow.position, speed * Time.deltaTime);
        }
    }

}
