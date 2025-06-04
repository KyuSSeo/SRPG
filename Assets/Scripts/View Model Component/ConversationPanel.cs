using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class ConversationPanel : MonoBehaviour
{
    //  ��ȭ
    public TextMeshProUGUI message;
    //  ��ü ������ �̹���
    public Image speaker;
    public GameObject arrow;
    public Panel panel;

    private void Start()
    {
        // ȭ��ǥ�� ���� ���� ��ġ
        Vector3 pos = arrow.transform.localPosition;
        
        // �ִϸ��̼� ���� ��ġ ����
        arrow.transform.localPosition = new Vector3(pos.x, pos.y + 5, pos.z);
       
        Tweener t = arrow.transform.MoveToLocal(
            new Vector3(pos.x, pos.y - 5, pos.z),
            0.5f, EasingEquations.EaseInQuad
            );

        // ȭ��ǥ �ִϸ��̼� ���� �ݺ�
        t.loopType = EasingControl.LoopType.PingPong;
        t.loopCount = -1;
    }

    //  ��ȭ ǥ�� �ڷ�ƾ
    public IEnumerator Display(SpeakerData sd)
    {
        //  ��ü �̹��� ����
        speaker.sprite = sd.speaker;
        // �̹��� ������ ����
        speaker.SetNativeSize();
        // ��� �޽��� �ϳ��� ǥ��
        for (int i = 0; i < sd.messages.Count; ++i)
        {
            message.text = sd.messages[i];
            // ������ �޽����� �ƴ� ��� ȭ��ǥ ǥ��
            arrow.SetActive(i + 1 < sd.messages.Count);
            // ���� ������ ���
            yield return null;
        }
    }
}