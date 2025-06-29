using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//  항목 표시, 건너뛰기 등을 처리
public class AbilityMenuPanelController : MonoBehaviour
{
    //  재상할 애니메이션 이름
    private const string ShowKey = "Show";
    private const string HideKey = "Hide";
    private const string EntryPoolKey = "AbilityMenuPanel.Entry";

    //  풀에서 사용할 매뉴 개수
    private const int MenuCount = 4;

    //  메뉴 항목
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TextMeshProUGUI titleLabel;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvas;

    //  메뉴 항목들을 저장하는 리스트
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);


    // 현재 선택된 항목 인덱스
    public int selection { get; private set; }

    private void Awake()
    {
        // 풀에 메뉴 항목 미리 등록
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }

    // 풀에서 꺼내기
    private AbilityMenuEntry Dequeue()
    {
        //  풀에서 가져오기
        Poolable p = GameObjectPoolController.Dequeue(EntryPoolKey);
        AbilityMenuEntry entry = p.GetComponent<AbilityMenuEntry>();
        
        //  패널에 적용하기
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
       
        //  활성화
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;
    }

    // 항목을 풀에 반환하기
    private void Enqueue(AbilityMenuEntry entry)
    {
        Poolable p = entry.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    // 모든 항목 반환 및 리스트 비우기
    private void Clear()
    {
        for (int i = menuEntries.Count - 1; i >= 0; --i)
            Enqueue(menuEntries[i]);
        menuEntries.Clear();
    }

    // 초기 설정 
    private void Start()
    {
        //  비활성화, 숨기기
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }

    // 패널 위치 전환 애니메이션
    private Tweener TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.duration = 0.5f;
        t.equation = EasingEquations.EaseOutQuad;
        return t;
    }

    // 선택 항목 변경
    private bool SetSelection(int value)
    {
        //  잠긴 항목은 선택 불가능
        if (menuEntries[value].IsLocked)
            return false;

        // 기존 선택 해제
        if (selection >= 0 && selection < menuEntries.Count)
            menuEntries[selection].IsSelected = false;

        selection = value;

        // 새 항목 선택
        if (selection >= 0 && selection < menuEntries.Count)
            menuEntries[selection].IsSelected = true;

        return true;
    }

    // 다음 선택 항목으로 이동
    public void Next()
    {
        for (int i = selection + 1; i < selection + menuEntries.Count; ++i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
                break;
        }
    }

    // 이전 선택 항목으로 이동
    public void Previous()
    {
        for (int i = selection - 1 + menuEntries.Count; i > selection; --i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
                break;
        }
    }

    // 메뉴 표시
    public void Show(string title, List<string> options)
    {
        // 메뉴 캔버스 활성화
        canvas.SetActive(true);
        Clear();
        titleLabel.text = title;
        // 항목 생성 및 초기화
        for (int i = 0; i < options.Count; ++i)
        {
            AbilityMenuEntry entry = Dequeue();
            entry.Title = options[i];
            menuEntries.Add(entry);
        }
        SetSelection(0);
        TogglePos(ShowKey);
    }
    //  잠금 설정
    public void SetLocked(int index, bool value)
    {
        if (index < 0 || index >= menuEntries.Count)
            return;

        // 선택 중인 항목이 잠기면 다음 항목으로 이동
        menuEntries[index].IsLocked = value;
        if (value && selection == index)
            Next();
    }

    // 메뉴 숨기기
    public void Hide()
    {
        Tweener t = TogglePos(HideKey);
        t.completedEvent += delegate (object sender, System.EventArgs e)
        {   
            //  비활성화
            if (panel.CurrentPosition == panel[HideKey])
            {
                Clear();
                canvas.SetActive(false);
            }
        };
    }
}

