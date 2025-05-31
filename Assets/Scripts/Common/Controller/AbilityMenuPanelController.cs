using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//  �׸� ǥ��, �ǳʶٱ� ���� ó��
public class AbilityMenuPanelController : MonoBehaviour
{
    //  ����� �ִϸ��̼� �̸�
    private const string showKey = "Show";
    private const string hideKey = "Hide";
    private const string entryPoolKey = "AbilityMenuPanel.Entry";

    //  �Ŵ� ����
    private const int MenuCount = 4;

    //  �޴� �׸�
    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TextMeshPro titleLable;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvus;

    //  �޴� �׸���� �����ϴ� ����Ʈ
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);
}

