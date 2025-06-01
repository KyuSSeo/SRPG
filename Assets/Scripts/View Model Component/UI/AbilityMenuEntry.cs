using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// ���� �ɷ� �޴� �׸� ����
public class AbilityMenuEntry : MonoBehaviour
{
    [SerializeField] private Image bullet;
    
    //  ����� ��������Ʈ 3���� �� 1�� ��
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite disabledSprite;
    
    [SerializeField] private TextMeshProUGUI label;

    //  ������
    private Outline outline;
    //   ���� ���� ����
    private States state;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public string Title
    {
        get { return label.text; }
        set { label.text = value; }
    }

    //  ��� �ִ��� Ȯ��
    public bool IsLocked
    {   
        get { return (State & States.Locked) != States.None; }
        set
        {   
            //  ��� ����, ����
            if (value)
                State |= States.Locked;     
            else
                State &= ~States.Locked;
        }
    }


    // ���õǾ����� Ȯ��
    public bool IsSelected
    {
        get { return (State & States.Selected) != States.None; }
        set
        {
            //  ���� ����, ����
            if (value)
                State |= States.Selected;
            else
                State &= ~States.Selected;
        }
    }


    private States State
    {
        get { return state; }
        set
        {
            //  ���� ���� �ʿ� ������ ����������
            if (state == value)
                return;
            state = value;

            //  ���¿� ���� �� ����
            if (IsLocked)
            {
                bullet.sprite = disabledSprite;
                label.color = Color.gray;
                outline.effectColor = new Color32(20, 36, 44, 255);
            }
            else if (IsSelected)
            {
                bullet.sprite = selectedSprite;
                label.color = new Color32(249, 210, 118, 255);
                outline.effectColor = new Color32(255, 160, 72, 255);
            }
            else
            {
                bullet.sprite = normalSprite;
                label.color = Color.white;
                outline.effectColor = new Color32(20, 36, 44, 255);
            }
        }
    }
    

    //  ���� �ʱ�ȭ�ϱ�
    public void Reset()
    {
        State = States.None;
    }

    //  �Ŵ� ���� ������
    //  ��Ʈ ���� ���� https://www.youtube.com/watch?v=IYAHieM4iZE
    [System.Flags]
    enum States
    {   
        //  �ƹ� ���µ� ������� �ʾ���
        None = 0,
        //  ���õ� ����
        Selected = 1 << 0,
        //  ��� ����
        Locked = 1 << 1

        // And ������ ���� None �� �������� � ���µ��� ����Ǿ����� Ȯ�� ����
        /*���� ����
          States.None	                    0000	�ƹ� ���� �ƴ�
          States.Selected	                0001	���õ�
          States.Locked	                    0010	���
          States.Selected, States.Locked	0011    ���õǰ� ���
        */
    }
}
