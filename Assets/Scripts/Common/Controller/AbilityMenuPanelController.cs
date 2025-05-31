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
    [SerializeField] private TextMeshPro titleLable;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvus;

    //  �޴� �׸���� �����ϴ� ����Ʈ
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);

    void Awake()
    {
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }
}

