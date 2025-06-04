using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class ConversationPanel : MonoBehaviour
{
    //  대화
    public TextMeshProUGUI message;
    //  객체 배정된 이미지
    public Image speaker;
    public GameObject arrow;
    public Panel panel;

    private void Start()
    {
        // 화살표의 현재 로컬 위치
        Vector3 pos = arrow.transform.localPosition;
        
        // 애니메이션 시작 위치 지정
        arrow.transform.localPosition = new Vector3(pos.x, pos.y + 5, pos.z);
       
        Tweener t = arrow.transform.MoveToLocal(
            new Vector3(pos.x, pos.y - 5, pos.z),
            0.5f, EasingEquations.EaseInQuad
            );

        // 화살표 애니메이션 무한 반복
        t.loopType = EasingControl.LoopType.PingPong;
        t.loopCount = -1;
    }

    //  대화 표시 코루틴
    public IEnumerator Display(SpeakerData sd)
    {
        //  객체 이미지 설정
        speaker.sprite = sd.speaker;
        // 이미지 사이즈 설정
        speaker.SetNativeSize();
        // 모든 메시지 하나씩 표시
        for (int i = 0; i < sd.messages.Count; ++i)
        {
            message.text = sd.messages[i];
            // 마지막 메시지가 아닐 경우 화살표 표시
            arrow.SetActive(i + 1 < sd.messages.Count);
            // 다음 프레임 대기
            yield return null;
        }
    }
}