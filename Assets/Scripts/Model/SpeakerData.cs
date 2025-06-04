using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  인스팩터 상에서 설정하기
[System.Serializable]
public class SpeakerData
{   
    //  한 객체가 말하는 모든 대사
    public List<string> messages;
    //  객체의 이미지 스프라이트
    public Sprite speaker;
    //  화면 표시 위치
    public TextAnchor anchor;
}
