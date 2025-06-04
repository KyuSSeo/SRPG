using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationController : MonoBehaviour
{
    //  ��ȭ ��ü
    [SerializeField] private ConversationPanel leftPanel;
    [SerializeField] private ConversationPanel rightPanel;
    
    private Canvas canvas;
    private IEnumerator conversation;
    private Tweener transition;
   
    // �ִϸ��̼� ��ġ �̸� ���
    private const string ShowTop = "Show Top";
    private const string ShowBottom = "Show Bottom";
    private const string HideTop = "Hide Top";
    private const string HideBottom = "Hide Bottom";
    
    // ��ȭ ���� �̺�Ʈ
    public static event EventHandler completeEvent;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        // �ʱ� �г� ��ġ�� ����
        if (leftPanel.panel.CurrentPosition == null)
            leftPanel.panel.SetPosition(HideBottom, false);
        if (rightPanel.panel.CurrentPosition == null)
            rightPanel.panel.SetPosition(HideBottom, false);
        // ĵ���� �⺻ ��Ȱ��
        canvas.gameObject.SetActive(false);
    }

    // ��ȭ ����,  ĵ���� Ȱ��ȭ
    public void Show(ConversationData data)
    {
        canvas.gameObject.SetActive(true);
        conversation = Sequence(data);
        conversation.MoveNext();
    }

    //  ���� �޽���
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

    // ��ȭ ������ ����
    private IEnumerator Sequence(ConversationData data)
    {
        for (int i = 0; i < data.list.Count; ++i)
        {
            // ���� ��ȭ ���� ��������
            SpeakerData sd = data.list[i];

            // ��ü ��� �г� ����
            ConversationPanel currentPanel = (sd.anchor == TextAnchor.UpperLeft || sd.anchor == TextAnchor.MiddleLeft || sd.anchor == TextAnchor.LowerLeft) ? leftPanel : rightPanel;

            // �޽��� ǥ�� �ڷ�ƾ
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
            // �г� ��ġ �ʱ�ȭ �� �ִϸ��̼�

            currentPanel.panel.SetPosition(hide, false);
            MovePanel(currentPanel, show);
            yield return null;

            // ��� �޽��� ǥ�� ���
            while (presenter.MoveNext())
                yield return null;
            MovePanel(currentPanel, hide);
            
            // ���� �޽��� ����
            transition.completedEvent += delegate (object sender, EventArgs e) {
                conversation.MoveNext();
            };
            yield return null;
        }
        // ��ȭ ���� �� ĵ���� ��Ȱ��
        canvas.gameObject.SetActive(false);

        // ��ȭ �Ϸ� �̺�Ʈ �߻�
        if (completeEvent != null)
            completeEvent(this, EventArgs.Empty);
    }
}
