using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuPanelController : MonoBehaviour
{
    private const string showKey = "Show";
    private const string hideKey = "Hide";
    private const string entryPoolKey = "AbilityMenuPanel.Entry";
    private int MenuCount = 4;

    [SerializeField] private GameObject entryPrefab;
    [SerializeField] private TextMeshPro titleLable;
    [SerializeField] private Panel panel;
    [SerializeField] private GameObject canvus;

    private List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(Menucoumt);
    public int selection { get; private set; }

}
    
