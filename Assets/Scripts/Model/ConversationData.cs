using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  https://docs.unity3d.com/kr/2022.3/Manual/class-ScriptableObject.html
[CreateAssetMenu(fileName = "New ConversationData", menuName = "Tactics RPG/Conversation Data")]
// SpeakerData�� ����� �����ϴ� �뵵
public class ConversationData : ScriptableObject
{
    public List<SpeakerData> list;
}
