using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// 개별 능력 메뉴 항목 관리
public class AbilityMenuEntry : MonoBehaviour
{
    [SerializeField] private Image bullet;
    
    //  사용할 스프라이트 3가지 중 1개 택
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite disabledSprite;
    
    [SerializeField] private TextMeshProUGUI label;

    //  윤곽선
    private Outline outline;
    //   현재 상태 저장
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

    //  잠겨 있는지 확인
    public bool IsLocked
    {   
        get { return (State & States.Locked) != States.None; }
        set
        {   
            //  잠금 설정, 해제
            if (value)
                State |= States.Locked;     
            else
                State &= ~States.Locked;
        }
    }


    // 선택되었는지 확인
    public bool IsSelected
    {
        get { return (State & States.Selected) != States.None; }
        set
        {
            //  선택 설장, 해제
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
            //  상태 갱신 필요 없으면 빠져나가기
            if (state == value)
                return;
            state = value;

            //  상태에 따른 색 설정
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
    

    //  상태 초기화하기
    public void Reset()
    {
        State = States.None;
    }

    //  매뉴 상태 열거형
    //  비트 연산 가능 https://www.youtube.com/watch?v=IYAHieM4iZE
    [System.Flags]
    enum States
    {   
        //  아무 상태도 적용되지 않았음
        None = 0,
        //  선택된 상태
        Selected = 1 << 0,
        //  잠긴 상태
        Locked = 1 << 1

        // And 연산을 통해 None 과 비교함으로 어떤 상태들이 적용되었는지 확인 가능
        /*상태 설정
          States.None	                    0000	아무 상태 아님
          States.Selected	                0001	선택됨
          States.Locked	                    0010	잠김
          States.Selected, States.Locked	0011    선택되고 잠김
        */
    }
}
