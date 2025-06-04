using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    //  대화 객체
    [SerializeField] private ConversationPanel leftPanel;
    [SerializeField] private ConversationPanel rightPanel;
    
    private Canvas canvas;
    private IEnumerator conversation;
    private Tweener transition;
   
    // 애니메이션 위치 이름 상수
    private const string ShowTop = "Show Top";
    private const string ShowBottom = "Show Bottom";
    private const string HideTop = "Hide Top";
    private const string HideBottom = "Hide Bottom";
    
    // 대화 종료 이벤트
    public static event EventHandler completeEvent;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        // 초기 패널 위치를 설정
        if (leftPanel.panel.CurrentPosition == null)
            leftPanel.panel.SetPosition(HideBottom, false);
        if (rightPanel.panel.CurrentPosition == null)
            rightPanel.panel.SetPosition(HideBottom, false);
        // 캔버스 기본 비활성
        canvas.gameObject.SetActive(false);
    }

    // 대화 시작,  캔버스 활성화
    public void Show(ConversationData data)
    {
        canvas.gameObject.SetActive(true);
        conversation = Sequence(data);
        conversation.MoveNext();
    }

    //  다음 메시지
    public void Next()
    {   
        if (conversation == null || transition != null)
            return;

        conversation.MoveNext();
    }
    private void MovePanel(ConversationPanel obj, string pos)
    {
        transition = obj.panel.SetPosition(pos, true);
        transition.duration = 0.5f;
        transition.equation = EasingEquations.EaseOutQuad;
    }

    // 대화 순차적 진행
    private IEnumerator Sequence(ConversationData data)
    {
        for (int i = 0; i < data.list.Count; ++i)
        {
            // 현재 대화 정보 가져오기
            SpeakerData sd = data.list[i];

            // 객체 사용 패널 결정
            ConversationPanel currentPanel = (sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.MiddleLeft || sd.anchor == TextAnchor.LowerLeft) ? leftPanel : rightPanel;

            // 메시지 표시 코루틴
            IEnumerator presenter = currentPanel.Display(sd);
            presenter.MoveNext();
           
            
            string show, hide;
            if (sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.UpperCenter || sd.anchor == TextAnchor.UpperRight)
            {
                show = ShowTop;
                hide = HideTop;
            }
            else
            {
                show = ShowBottom;
                hide = HideBottom;
            }
            // 패널 위치 초기화 후 애니메이션

            currentPanel.panel.SetPosition(hide, false);
            MovePanel(currentPanel, show);
            yield return null;

            // 모든 메시지 표시 대기
            while (presenter.MoveNext())
                yield return null;
            MovePanel(currentPanel, hide);
            
            // 다음 메시지 진행
            transition.completedEvent += delegate (object sender, EventArgs e) {
                conversation.MoveNext();
            };
            yield return null;
        }
        // 대화 종료 후 캔버스 비활성
        canvas.gameObject.SetActive(false);

        // 대화 완료 이벤트 발생
        if (completeEvent != null)
            completeEvent(this, EventArgs.Empty);
    }
}
