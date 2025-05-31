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
    [SerializeField] private TextMeshPro titleLable;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvus;

    //  메뉴 항목들을 저장하는 리스트
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);

    void Awake()
    {
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }
}

