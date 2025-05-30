using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New ConversationData", menuName = "Tactics RPG/Conversation Data")]
// SpeakerData의 목록을 저장하는 용도
public class ConversationData : ScriptableObject
{
    public List<SpeakerData> list;
}
