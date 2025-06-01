using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//  �׸� ǥ��, �ǳʶٱ� ���� ó��
public class AbilityMenuPanelController : MonoBehaviour
{
    //  ����� �ִϸ��̼� �̸�
    private const string ShowKey = "Show";
    private const string HideKey = "Hide";
    private const string EntryPoolKey = "AbilityMenuPanel.Entry";

    //  Ǯ���� ����� �Ŵ� ����
    private const int MenuCount = 4;

    //  �޴� �׸�
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TextMeshPro titleLabel;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvas;

    //  �޴� �׸���� �����ϴ� ����Ʈ
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);


    // ���� ���õ� �׸� �ε���
    public int selection { get; private set; }

    private void Awake()
    {
        // Ǯ�� �޴� �׸� �̸� ���
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }

    // Ǯ���� ������
    private AbilityMenuEntry Dequeue()
    {
        //  Ǯ���� ��������
        Poolable p = GameObjectPoolController.Dequeue(EntryPoolKey);
        AbilityMenuEntry entry = p.GetComponent<AbilityMenuEntry>();
        
        //  �гο� �����ϱ�
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
       
        //  Ȱ��ȭ
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;
    }

    // �׸��� Ǯ�� ��ȯ�ϱ�
    private void Enqueue(AbilityMenuEntry entry)
    {
        Poolable p = entry.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    // ��� �׸� ��ȯ �� ����Ʈ ����
    private void Clear()
    {
        for (int i = menuEntries.Count - 1; i >= 0; --i)
            Enqueue(menuEntries[i]);
        menuEntries.Clear();
    }

    // �ʱ� ���� 
    private void Start()
    {
        //  ��Ȱ��ȭ, �����
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }

    // �г� ��ġ ��ȯ �ִϸ��̼�
    private Tweener TogglePos(string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.easingControl.duration = 0.5f;
        t.easingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }

    // ���� �׸� ����
    private bool SetSelection(int value)
    {
        //  ��� �׸��� ���� �Ұ���
        if (menuEntries[value].IsLocked)
            return false;

        // ���� ���� ����
        if (selection >= 0 && selection < menuEntries.Count)
            menuEntries[selection].IsSelected = false;

        selection = value;

        // �� �׸� ����
        if (selection >= 0 && selection < menuEntries.Count)
            menuEntries[selection].IsSelected = true;

        return true;
    }

    // ���� ���� �׸����� �̵�
    public void Next()
    {
        for (int i = selection + 1; i < selection + menuEntries.Count; ++i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
                break;
        }
    }

    // ���� ���� �׸����� �̵�
    public void Previous()
    {
        for (int i = selection - 1 + menuEntries.Count; i > selection; --i)
        {
            int index = i % menuEntries.Count;
            if (SetSelection(index))
                break;
        }
    }

    // �޴� ǥ��
    public void Show(string title, List<string> options)
    {
        // �޴� ĵ���� Ȱ��ȭ
        canvas.SetActive(true);
        Clear();
        titleLabel.text = title;
        // �׸� ���� �� �ʱ�ȭ
        for (int i = 0; i < options.Count; ++i)
        {
            AbilityMenuEntry entry = Dequeue();
            entry.Title = options[i];
            menuEntries.Add(entry);
        }
        SetSelection(0);
        TogglePos(ShowKey);
    }
    //  ��� ����
    public void SetLocked(int index, bool value)
    {
        if (index < 0 || index >= menuEntries.Count)
            return;

        // ���� ���� �׸��� ���� ���� �׸����� �̵�
        menuEntries[index].IsLocked = value;
        if (value && selection == index)
            Next();
    }

    // �޴� �����
    public void Hide()
    {
        Tweener t = TogglePos(HideKey);
        t.easingControl.completedEvent += delegate (object sender, System.EventArgs e)
        {   
            //  ��Ȱ��ȭ
            if (panel.CurrentPosition == panel[HideKey])
            {
                Clear();
                canvas.SetActive(false);
            }
        };
    }
}

