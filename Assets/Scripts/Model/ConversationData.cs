using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New ConversationData", menuName = "Tactics RPG/Conversation Data")]
// SpeakerData�� ����� �����ϴ� �뵵
public class ConversationData : ScriptableObject
{
    public List<SpeakerData> list;
}
